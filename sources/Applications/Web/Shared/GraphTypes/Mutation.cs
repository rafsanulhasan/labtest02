using HotChocolate;
using HotChocolate.Subscriptions;

using LabTest2.Apps.Web.Shared.DTOs;
using LabTest2.Apps.Web.Shared.Services;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class Mutation
	{
		public async Task<WeatherForecastRequestPayload> AddForecastAsync(
			WeatherForecastDTO weatherForecast,
			[Service] IWeatherForecastService forecastService,
			[Service] ITopicEventSender eventSender,
			CancellationToken token
		)
		{
			forecastService.AddForecast(weatherForecast);
			await eventSender.SendAsync("weatherForecast", weatherForecast, token);
			await eventSender.SendAsync("getForecastCompleted", weatherForecast, token);

			return new WeatherForecastRequestPayload("Inserted");
		}

		public async Task<WeatherForecastRequestPayload> AddForecastsAsync(
			IEnumerable<WeatherForecastDTO> weatherForecastDTO,
			[Service] IWeatherForecastService forecastService,
			[Service] ITopicEventSender eventSender,
			CancellationToken token
		)
		{
			forecastService.AddForecast(weatherForecastDTO);
			foreach (var f in weatherForecastDTO)
			{
				await eventSender.SendAsync("weatherForecast", f, token);
				await eventSender.SendAsync("getForecastCompleted", f, token);
			}

			return new WeatherForecastRequestPayload("all inserted");
		}
	}
}
