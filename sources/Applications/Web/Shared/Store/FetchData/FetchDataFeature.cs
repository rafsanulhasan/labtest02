using Fluxor;

using LabTest2.Apps.Web.Shared.DTOs;

using System.Collections.Immutable;

namespace LabTest2.Apps.Web.Shared.Store.FetchData
{
	public class FetchDataFeature
		: Feature<FetchDataState>
	{
		public override string GetName()
			=> "FetchData";

		protected override FetchDataState GetInitialState() 
			=> new FetchDataState(
				   ImmutableList<WeatherForecastDTO>.Empty,
				   true
			   );
	}
}
