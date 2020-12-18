using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.DTOs;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
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