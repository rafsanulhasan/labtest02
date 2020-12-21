using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.Store.FetchData;

namespace LabTest2.Apps.Web.Client.Store
{
	public abstract record StateBase();

	public record AppState(
		CounterState Counter,
		FetchDataState FetchDataState
	) : StateBase;
}
