
using System.Collections.Generic;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{
	public abstract class PayloadBase
	{
		public IReadOnlyList<UserError>? Errors { get; }

		protected PayloadBase(IReadOnlyList<UserError> errors = null) 
			=> Errors = errors ?? new List<UserError>();
	}
}
