using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

using ReactiveUI;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class FetchData
		: RxUIFluxorComponent<FetchDataViewModel, FetchDataState>
	{
		protected override void OnInitialized()
		{
			base.OnInitialized();

			this
				.WhenAnyValue(v => v.ViewModel)
				.Where(vm => vm is not null)
				.Select(vm => vm.WeatherForecasts)
				.Where(forecasts => forecasts is not null)
				.Select(forecasts => forecasts.Count == 0)
				.ToProperty(
					ViewModel,
					vm => vm.IsEmpty,
					() => ViewModel.State.Value.WeatherForecasts.Count == 0
				)
				.DisposeWith(Disposables);

			this
				.WhenAnyValue(v => v.ViewModel)
				.Where(vm => vm is not null)
				.Select(vm=>vm.State.Value)
				.Where(state => state is not null)
				.Select(state => state.IsLoading)
				.ToProperty(
					ViewModel,
					vm => vm.IsLoading,
					() => ViewModel.State.Value.IsLoading
				)
				.DisposeWith(Disposables);

			ViewModel
				.FetchCommand
				.Execute()
				.Subscribe();
		}
	}
}
