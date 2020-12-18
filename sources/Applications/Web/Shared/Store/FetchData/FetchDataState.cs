using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.DTOs;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public record FetchDataState(
		IImmutableList<WeatherForecastDTO> WeatherForecasts,
		bool IsLoading
	) : StateBase;
}
