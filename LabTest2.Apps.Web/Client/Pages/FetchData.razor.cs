
using System.Reactive.Disposables;

using Fluxor;

using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class FetchData
		: RxUIFluxorComponent<FetchDataViewModel>
	{
		[Inject]
		public IDispatcher Dispatcher { get; private set; }

		[Inject]
		public IStore Store { get; private set; }

		[Inject]
		public IState<FetchDataState> State { get; private set; }

		protected override void OnInitialized()
		{
			base.OnInitialized();
			ViewModel = new FetchDataViewModel(
				Store,
				State,
				Dispatcher
			);
			ViewModel.DisposeWith(Disposables);
		}
	}
}
