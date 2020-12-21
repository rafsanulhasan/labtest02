using LabTest2.Apps.Web.Shared.DTOs;

using System.Collections.Immutable;

namespace LabTest2.Apps.Web.Client.Store.FetchData
{
	public record FetchDataAction();
	public record FetchDataResultAction
	{
		public IImmutableList<WeatherForecastDTO> WeatherForecasts { get; init; }
		public bool IsLoading { get; init; }

		public FetchDataResultAction(
			IImmutableList<WeatherForecastDTO> weatherForecasts,
			bool isLoading
		)
		{
			WeatherForecasts = weatherForecasts;
			IsLoading = isLoading;
		}
	}
}