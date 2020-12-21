using LabTest2.Apps.Web.Shared.DTOs;

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.Services
{
	public class WeatherForecastService
		: IDisposable, IWeatherForecastService
	{
		private IImmutableList<WeatherForecastDTO> _staticData;
		private readonly ISubject<WeatherForecastDTO> _weatherSubject;
		private readonly IObservable<WeatherForecastDTO> _weatherObservable;
		private string[] _summeries;

		private readonly CompositeDisposable _disposables;
		private bool _disposedValue;

		private IEnumerable<WeatherForecastDTO> GetInitialData()
		{
			var rng = new Random();
			return Enumerable.Range(1, 15).Select(index => new WeatherForecastDTO
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = _summeries[rng.Next(_summeries.Length)]
			});
		}

		public WeatherForecastService()
		{
			_disposables = new CompositeDisposable();
			_weatherSubject = new BehaviorSubject<WeatherForecastDTO>(null);
			_summeries = new[]
			{
				"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
			};
			_staticData = GetInitialData().ToImmutableList();

			_weatherObservable = Observable.Create<WeatherForecastDTO>(async obs =>
			{
				try
				{
					foreach (var data in _staticData)
						obs.OnNext(data);
					obs.OnCompleted();
				}
				catch (Exception ex)
				{
					obs.OnError(ex);
				}
				return Disposable.Empty;
			});
		}

		public IObservable<WeatherForecastDTO> ObserveForecasts() 
			=> _weatherObservable;

		public IAsyncEnumerable<WeatherForecastDTO> GetForecasts() 
			=> _staticData.ToAsyncEnumerable();

		public void AddForecast(WeatherForecastDTO dto)
		{
			try
			{
				_weatherSubject.OnNext(dto);
				_weatherSubject.OnCompleted();

				var data = _staticData.ToList();
				data.Add(dto);
				_staticData = data.ToImmutableList();
			}
			catch (Exception ex)
			{
				_weatherSubject.OnError(ex);
			}
		}

		public void AddForecast(IEnumerable<WeatherForecastDTO> dtos)
		{
			try
			{
				var data = _staticData.ToList();
				foreach (var dto in dtos)
				{
					_weatherSubject.OnNext(dto);
					data.Add(dto);
				}
				_weatherSubject.OnCompleted();
				_staticData = data.ToImmutableList();
			}
			catch (Exception ex)
			{
				_weatherSubject.OnError(ex);
			}
		}

		public async Task AddForecast(IAsyncEnumerable<WeatherForecastDTO> dtos)
		{
			try
			{
				var data = _staticData.ToList();
				await foreach (var dto in dtos)
				{
					_weatherSubject.OnNext(dto);
					data.Add(dto);
				}
				_weatherSubject.OnCompleted();
				_staticData = data.ToImmutableList();
			}
			catch (Exception ex)
			{
				_weatherSubject.OnError(ex);
			}
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					_disposables?.Dispose();
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
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
