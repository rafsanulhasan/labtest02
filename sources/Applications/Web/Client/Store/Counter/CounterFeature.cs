
using Fluxor;

namespace LabTest2.Apps.Web.Client.Store.Counter
{
	public class CounterFeature
		: Feature<CounterState>
	{
		public override string GetName()
			=> "Counter";

		protected override CounterState GetInitialState()
			=> new CounterState(0);
	}
}
