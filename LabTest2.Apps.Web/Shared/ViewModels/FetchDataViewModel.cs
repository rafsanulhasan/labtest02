using System;
using System.Collections.Immutable;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using Fluxor;

using LabTest2.Apps.Web.Shared.Models;
using LabTest2.Apps.Web.Shared.Store.FetchData;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

using ReactiveUI;

namespace LabTest2.Apps.Web.Shared.ViewModels
{
	public class FetchDataViewModel
		: ViewModelBase<FetchDataState>
	{
		private IImmutableList<WeatherForecast> _weatherForecasts;
		public IImmutableList<WeatherForecast> WeatherForecasts
		{
			get => _weatherForecasts;
			set => this.RaiseAndSetIfChanged(ref _weatherForecasts, value);
		}

		public ReactiveCommand<Unit, Unit> FetchCommand { get; init; }

		private bool _isLoading;
		public bool IsLoading
		{
			get => _isLoading;
			set => this.RaiseAndSetIfChanged(ref _isLoading, value);
		}

		private bool _isEmpty;
		public bool IsEmpty
		{
			get => _isEmpty;
			set => this.RaiseAndSetIfChanged(ref _isEmpty, value);
		}

		private bool _canLoad;
		public bool CanLoad
		{
			get => _canLoad;
			set => this.RaiseAndSetIfChanged(ref _canLoad, value);
		}

		private string _error;
		public string Error
		{
			get => _error;
			set => this.RaiseAndSetIfChanged(ref _error, value);
		}

		public FetchDataViewModel(
			IStore store,
			IState<FetchDataState> state,
			IDispatcher dispatcher
		)
			: base(store, state, dispatcher)
		{
			var vmState = State.Value;
			WeatherForecasts = vmState.WeatherForecasts;

			Observable.Create<bool>(o =>
			{
				try
				{
					o.OnNext(
						WeatherForecasts.Count == 0
					);
					o.OnCompleted();
				}
				catch (Exception ex)
				{
					o.OnError(ex);
				}
				return Disposable.Empty;
			})
			.StartWith(true)
			.ToProperty(
				this,
				vm => vm.IsEmpty,
				() => WeatherForecasts.Count == 0
			);

			Observable.Create<bool>(o =>
			{
				try
				{
					o.OnNext(vmState.IsLoading);
					o.OnCompleted();
				}
				catch (Exception ex)
				{
					o.OnError(ex);
				}
				return Disposable.Empty;
			})
			.StartWith(true)
			.ToProperty(
				this,
				vm => vm.IsLoading,
				() => vmState.IsLoading
			);

			FetchCommand = ReactiveCommand.Create(
				() => GetForecasts(),
				null,
				RxApp.MainThreadScheduler
			)
			.DisposeWith(Disposables);

			_ = Observable.FromEventPattern<FetchDataState>(
				eh => State.StateChanged += eh,
				eh => State.StateChanged -= eh
			)
			.Where(evt => evt is not null)
			.Select(handler => handler.EventArgs)
			.Where(args => args is not null)
			.Subscribe(state
				=> WeatherForecasts = state.WeatherForecasts
			)
			.DisposeWith(Disposables)
			;

			GetForecasts();
		}

		public void GetForecasts()
			=> Dispatcher.Dispatch(new FetchDataAction());
	}
}
