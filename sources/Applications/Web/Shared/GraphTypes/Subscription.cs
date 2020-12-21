using HotChocolate;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using HotChocolate.Types;

using LabTest2.Apps.Web.Shared.DTOs;

using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public class Subscription
	{
		[SubscribeAndResolve]
		public async ValueTask<ISourceStream<WeatherForecastDTO>> OnForecastedAsync(
			WeatherForecastDataLoader loader,
			[Service] ITopicEventReceiver topicEventReceiver,
			CancellationToken cancellationToken
		) 
			=> await topicEventReceiver.SubscribeAsync<string, WeatherForecastDTO>(
				"weatherForecast",
				cancellationToken
			);
		[SubscribeAndResolve]
		public async ValueTask<ISourceStream<WeatherForecastDTO>> OnGetForecastsAsync(
			WeatherForecastDataLoader loader,
			[Service] ITopicEventReceiver topicEventReceiver,
			CancellationToken cancellationToken
		)
			=> await topicEventReceiver.SubscribeAsync<string, WeatherForecastDTO>(
				"getForecastCompleted",
				cancellationToken
			);
	}
}
