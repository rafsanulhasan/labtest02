
using Fluxor;

namespace LabTest2.Apps.Web.Shared.Store.FetchDataService
{
	public class FetchDataServiceReducer
		: Reducer<FetchDataServiceState, FetchDataServiceResultAction>
	{
		public override FetchDataServiceState Reduce(
			FetchDataServiceState state,
			FetchDataServiceResultAction action
		)
			=> state with
			{
				WeatherForecasts = action.WeatherForecasts,
				IsLoading = action.IsLoading
			};
		//=> new FetchDataState(action.WeatherForecasts, action.IsLoading);
	}
}
