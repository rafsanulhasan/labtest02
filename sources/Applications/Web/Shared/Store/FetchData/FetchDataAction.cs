using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.Models;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public record FetchDataAction();
	public record FetchDataResultAction
	{
		public IImmutableList<WeatherForecast> WeatherForecasts { get; init; }
		public bool IsLoading { get; init; }

		public FetchDataResultAction(
			IImmutableList<WeatherForecast> weatherForecasts,
			bool isLoading
		)
		{
			WeatherForecasts = weatherForecasts;
			IsLoading = isLoading;
		}
	}
}