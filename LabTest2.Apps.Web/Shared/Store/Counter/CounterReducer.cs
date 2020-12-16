﻿
using Fluxor;

namespace LabTest2.Apps.Web.Shared.Store.Counter
{
	public class CounterIncrementReducer
		: Reducer<CounterState, CounterIncrementAction>
	{
		public override CounterState Reduce(
			CounterState state,
			CounterIncrementAction action
		)
			=> new CounterState(state.Count + action.Count);
	}
}
