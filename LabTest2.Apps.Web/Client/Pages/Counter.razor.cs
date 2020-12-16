using System;
using System.Reactive.Disposables;

using Fluxor;

using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

using Syncfusion.Blazor.Buttons;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class Counter
		: RxUIFluxorComponent<CounterViewModel>
	{
		[Inject]
		public IDispatcher Dispatcher { get; private set; }

		[Inject]
		public IStore Store { get; private set; }

		[Inject]
		public IState<CounterState> State { get; private set; }

		public SfButton IncreaseButton { get; set; }

		private void OnIncrease(MouseEventArgs _)
			=> ViewModel
				.IncreaseCommand
				.Execute()
				.Subscribe()
				.DisposeWith(Disposables);

		protected override void OnInitialized()
		{
			base.OnInitialized();
			ViewModel = new CounterViewModel(Store, State, Dispatcher);
			ViewModel.DisposeWith(Disposables);
		}

		protected override void OnAfterRender(bool firstRender)
		{
			base.OnAfterRender(firstRender);
			if (firstRender)
			{
				IncreaseButton?.DisposeWith(Disposables);
			}
		}
	}
}
