using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.Store.FetchData;

namespace LabTest2.Apps.Web.Shared.Store
{
	public record AppState(
		CounterState Counter,
		FetchDataState FetchDataState
	);
}
