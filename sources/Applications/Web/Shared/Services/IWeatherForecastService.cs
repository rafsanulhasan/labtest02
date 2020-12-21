using LabTest2.Apps.Web.Shared.DTOs;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.Services
{
	public interface IWeatherForecastService
	{
		Task AddForecast(IAsyncEnumerable<WeatherForecastDTO> dtos);
		void AddForecast(IEnumerable<WeatherForecastDTO> dtos);
		void AddForecast(WeatherForecastDTO dto);
		void Dispose();
		IObservable<WeatherForecastDTO> ObserveForecasts();
		IAsyncEnumerable<WeatherForecastDTO> GetForecasts();
	}
}