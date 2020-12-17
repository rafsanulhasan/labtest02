
using Fluxor;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public class FetchDataReducer
		: Reducer<FetchDataState, FetchDataResultAction>
	{
		public override FetchDataState Reduce(
			FetchDataState state,
			FetchDataResultAction action
		)
			=> state with
			{
				WeatherForecasts = action.WeatherForecasts,
				IsLoading = action.IsLoading
			};
		//=> new FetchDataState(action.WeatherForecasts, action.IsLoading);
	}
}
