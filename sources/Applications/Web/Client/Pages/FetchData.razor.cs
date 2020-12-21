using LabTest2.Apps.Web.Client.Shared;
using LabTest2.Apps.Web.Shared.DTOs;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components.Web.Virtualization;

using ReactiveUI;

using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Reactive.Concurrency;
using System.Threading;

namespace LabTest2.Apps.Web.Client.Pages
{
	public partial class FetchData
		: RxUIFluxorComponent<FetchDataViewModel, FetchDataState>
	{
		public ItemsProviderResult<WeatherForecastDTO> WeatherForecasts { get; protected set; }

		public FetchData()
		{
			WeatherForecasts = new ItemsProviderResult<WeatherForecastDTO>();
		}

		protected override void OnInitialized()
		{
			base.OnInitialized();

			WeatherForecasts = new ItemsProviderResult<WeatherForecastDTO>(State.Value.WeatherForecasts, 5);

			_ = Observable.FromEventPattern<FetchDataState>(
				eh => ViewModel.State.StateChanged += eh,
				eh => ViewModel.State.StateChanged -= eh
			)
			.Select(handler => handler.EventArgs)
			.Where(args => args is not null)
			.Select(args => args.WeatherForecasts)
			.Where(forecasts => forecasts is not null)
			.Where(forecasts => forecasts.Count > 0)
			.SubscribeOn(ThreadPoolScheduler.Instance)
			.ObserveOn(CurrentThreadScheduler.Instance)
			.Subscribe(forecasts =>
			{
				WeatherForecasts = new ItemsProviderResult<WeatherForecastDTO>(forecasts, 5);
				StateHasChanged();
			})
			.DisposeWith(Disposables);

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
				.Select(vm => vm.State.Value)
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

		public async ValueTask<ItemsProviderResult<WeatherForecastDTO>> LoadForecasts(
			ItemsProviderRequest request
		)
		{
			return await ValueTask
				.FromResult(WeatherForecasts);
		}
	}
}
