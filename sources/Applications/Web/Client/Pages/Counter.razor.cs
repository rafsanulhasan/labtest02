using System;
using System.Reactive.Disposables;

using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components.Web;

using Syncfusion.Blazor.Buttons;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class Counter
		: RxUIFluxorComponent<CounterViewModel, CounterState>
	{
		public SfButton IncreaseButton { get; set; }

		private void OnIncrease(MouseEventArgs _)
			=> ViewModel
				.IncreaseCommand
				.Execute()
				.Subscribe();

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
