using HotChocolate;
using HotChocolate.Subscriptions;

using LabTest2.Apps.Web.Shared.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class Query
	{
		public async ValueTask<WeatherForecastRequestPayload> GetForecasts(
			[Service] IWeatherForecastService forecastService,
			[Service] ITopicEventSender eventSender,
			CancellationToken token
		)
		{
			WeatherForecastRequestPayload response;
			try
			{
				var forecasts = forecastService
					.GetForecasts();
					
				await foreach (var f in forecasts)
				{
					await eventSender.SendAsync("getForecastCompleted", f, token);
				}
				response = new WeatherForecastRequestPayload("Event Execcuted");
			}
			catch(Exception ex)
			{
				response = new WeatherForecastRequestPayload(
					new[] { 
						new UserError(ex.Message, Guid.NewGuid().ToString())
					}
				);
			}
			return response;
		}
	}
}
