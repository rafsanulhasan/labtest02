﻿using System;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

using Fluxor;
using Fluxor.UnsupportedClasses;

using LabTest2.Apps.Web.Shared.Store;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components;

using ReactiveUI;
using ReactiveUI.Blazor;

namespace LabTest2.Apps.Web.Client.Shared
{
	public class RxUIFluxorComponent<TViewModel, TState>
		: ReactiveComponentBase<TViewModel>
		where TViewModel : ViewModelBase<TState>
		where TState : StateBase
	{
		private IDisposable _stateSubscription;
		private readonly ThrottledInvoker _stateHasChangedThrottler;

		protected bool Disposed;

		[Inject]
		private IActionSubscriber ActionSubscriber { get; set; }

		[Inject]
		public new TViewModel ViewModel { get; protected set; }

		[Inject]
		public IServiceProvider ServiceProvider { get; protected set; }

		public CompositeDisposable Disposables { get; protected set; }

		[Inject]
		public IStore Store { get; protected set; }

		[Inject]
		public IState<TState> State { get; protected set; }

		[Inject]
		public IDispatcher Dispatcher { get; protected set; }

		/// <summary>
		/// If greater than 0, the feature will not execute state changes
		/// more often than this many times per second. Additional notifications
		/// will be surpressed, and observers will be notified of the latest
		/// state when the time window has elapsed to allow another notification.
		/// </summary>
		protected byte MaximumStateChangedNotificationsPerSecond { get; set; }

		/// <summary>
		/// Creates a new instance
		/// </summary>
		public RxUIFluxorComponent()
		{
			Disposables = new CompositeDisposable();
			_stateHasChangedThrottler = new ThrottledInvoker(() =>
			{
				if (!Disposed)
				{
					InvokeAsync(StateHasChanged);
				}
			});

			this
				.WhenAnyValue(v => v.ViewModel)
				.Where(vm => vm is not null)
				.Select(vm =>
				    Observable
					    .FromEvent<PropertyChangedEventHandler, Unit>(
						    handler =>
						    {
							    void Handler(object sender, PropertyChangedEventArgs e)
								    => handler(Unit.Default);
							    return Handler;
						    },
						    eh => vm.PropertyChanged += eh,
						    eh => vm.PropertyChanged -= eh
					    )
				)
				.Switch()
				.Do(_ =>
				{
					if (!Disposed)
					{
						InvokeAsync(StateHasChanged);
					}
				})
				.Subscribe()
				.DisposeWith(Disposables)
				;
		}

		/// <summary>
		/// Subscribe
		/// </summary>
		/// <see cref="IActionSubscriber.SubscribeToAction{TAction}(object, Action{TAction})"/>
		public void SubscribeToAction<TAction>(Action<TAction> callback)
			=> ActionSubscriber
				.SubscribeToAction<TAction>(
					this,
					action =>
					{
						if (!Disposed)
						{
							callback(action);
						}
					}
				);

		/// <summary>
		/// Subscribes to state properties
		/// </summary>
		protected override void OnInitialized()
		{
			base.OnInitialized();
			_stateSubscription = StateSubscriber.Subscribe(
				this,
				_ => _stateHasChangedThrottler.Invoke(MaximumStateChangedNotificationsPerSecond)
			)
			.DisposeWith(Disposables);

			ViewModel.DisposeWith(Disposables);
		}

		/// <summary>
		/// Subscribes to state properties
		/// </summary>
		protected override async Task OnInitializedAsync()
		{
			await base.OnInitializedAsync().ConfigureAwait(false);
			_stateSubscription = StateSubscriber.Subscribe(
				this,
				_ => _stateHasChangedThrottler.Invoke(MaximumStateChangedNotificationsPerSecond)
			)
			.DisposeWith(Disposables);
			ViewModel.DisposeWith(Disposables);
		}

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (!Disposed)
			{
				Disposed = true;
				if (disposing)
				{
					Disposables?.Dispose();
					ActionSubscriber?.UnsubscribeFromAllActions(this);
				}
			}
		}
	}
}
