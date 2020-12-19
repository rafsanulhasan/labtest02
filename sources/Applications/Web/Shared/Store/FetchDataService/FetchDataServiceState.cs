using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.DTOs;

namespace LabTest2.Apps.Web.Shared.Store.FetchDataService
{
	public record FetchDataServiceState(
		IImmutableList<WeatherForecastDTO> WeatherForecasts,
		bool IsLoading
	) : StateBase;
}
