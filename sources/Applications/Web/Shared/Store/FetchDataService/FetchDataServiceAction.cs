using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.DTOs;

namespace LabTest2.Apps.Web.Shared.Store.FetchDataService
{
	public record FetchDataServiceAction();
	public record FetchDataServiceResultAction
	{
		public IImmutableList<WeatherForecastDTO> WeatherForecasts { get; init; }
		public bool IsLoading { get; init; }

		public FetchDataServiceResultAction(
			IImmutableList<WeatherForecastDTO> weatherForecasts,
			bool isLoading
		)
		{
			WeatherForecasts = weatherForecasts;
			IsLoading = isLoading;
		}
	}
}