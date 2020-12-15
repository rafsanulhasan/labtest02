using System;

using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components.Web;

using ReactiveUI;
using ReactiveUI.Blazor;

using Syncfusion.Blazor.Buttons;
using System.Reactive;
using System.Reactive.Linq;
using System.ComponentModel;
using System.Reactive.Disposables;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class Counter
		: ReactiveComponentBase<CounterViewModel>
	{
		private readonly CompositeDisposable _disposables;
		public SfButton IncreaseButton { get; set; }
		public bool IsIncreaseButtonDisabled { get; private set; }

		private void OnIncrease(MouseEventArgs _)
			=> ViewModel
				.IncreaseCommand
				.Execute()
				.Subscribe()
				.DisposeWith(_disposables);

		public Counter()
		{
			ViewModel = new CounterViewModel();
			_disposables = new CompositeDisposable();
			ViewModel.DisposeWith(_disposables);
			this
				.WhenAnyValue(v => v.ViewModel)
				.Where(vm => vm != null)
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
				.Do(_ => InvokeAsync(StateHasChanged))
				.Subscribe()
				.DisposeWith(_disposables)
				;
		}

		protected override void OnAfterRender(bool firstRender) 
			=> IncreaseButton.DisposeWith(_disposables);

		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (disposing)
			{
				_disposables?.Dispose();
			}
		}
	}
}
