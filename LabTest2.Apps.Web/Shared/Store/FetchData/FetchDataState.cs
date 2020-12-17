using System.Collections.Immutable;

using LabTest2.Apps.Web.Shared.Models;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public record FetchDataState(
		IImmutableList<WeatherForecast> WeatherForecasts,
		bool IsLoading
	) : StateBase;
}
