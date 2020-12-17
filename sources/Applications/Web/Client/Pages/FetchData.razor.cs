using System;
using System.Reactive.Disposables;

using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class FetchData
		: RxUIFluxorComponent<FetchDataViewModel, FetchDataState>
	{
		protected override void OnInitialized()
		{
			base.OnInitialized();
			ViewModel
				.FetchCommand
				.Execute()
				.Subscribe();
		}
	}
}
