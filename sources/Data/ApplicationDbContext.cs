
using IdentityServer4.EntityFramework.Options;

using LabTest2.Data.Entities;

using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LabTest2.Data
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
