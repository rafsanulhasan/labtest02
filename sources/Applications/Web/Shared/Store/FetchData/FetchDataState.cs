using LabTest2.Apps.Web.Shared.DTOs;

using System.Collections.Immutable;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public record FetchDataState(
		IImmutableList<WeatherForecastDTO> WeatherForecasts,
		bool IsLoading
	) : StateBase;
}
