
using IdentityServer4.EntityFramework.Options;

using LabTest2.Apps.Web.Server.Models;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LabTest2.Apps.Web.Server.Data
{
	public class ApplicationDbContext
		: ApiAuthorizationDbContext<ApplicationUser>
	{
		public ApplicationDbContext(
		    DbContextOptions options,
		    IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions
		)
		{
		}
	}
}
