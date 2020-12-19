using Fluxor;

using LabTest2.Apps.Web.Shared.DTOs;
using LabTest2.Apps.Web.Shared.Services;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Reactive.Disposables;

namespace LabTest2.Apps.Web.Shared.Store.FetchDataService
{
	public class FetchDataServiceFeature
		: Feature<FetchDataServiceState>, IDisposable
	{
		private IWeatherForecastService _forecastService;
		private bool _disposedValue;
		private readonly CompositeDisposable _disposables;

		public FetchDataServiceFeature(
			IWeatherForecastService forecastService
		)
		{
			_forecastService = forecastService;
			_disposables = new CompositeDisposable();
		}

		public override string GetName()
			=> "FetchDataService";

		protected override FetchDataServiceState GetInitialState()
		{
			var forecasts = new List<WeatherForecastDTO>();
			_forecastService
				.ObserveForecasts()
				.Subscribe(f => forecasts.Add(f))
				.DisposeWith(_disposables);
			return new FetchDataServiceState(
				   forecasts.ToImmutableList(),
				   false
			);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_disposables?.Dispose();
				}

				_disposedValue = true;
			}
		}

		public void Dispose()
		{
			// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
	}
}