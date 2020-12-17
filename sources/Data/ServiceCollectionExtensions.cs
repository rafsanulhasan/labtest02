using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;

namespace LabTest2.Data
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddDatabaseAndEF<TDbContext, TUser>(
			this IServiceCollection services,
			string connectionStringName,
			string migrationAssemblyName = null
		)
			where TDbContext : ApiAuthorizationDbContext<TUser>
			where TUser : IdentityUser
		{
			using (var sp = services.BuildServiceProvider())
			{
				var configuration = sp.GetRequiredService<IConfiguration>();
				var hostEnvironment = sp.GetRequiredService<IHostEnvironment>();
				services
					.AddDbContext<TDbContext>(options =>
						options.UseSqlServer(
							configuration
								.GetConnectionString("DefaultConnection")
							)
					);

				if (hostEnvironment.IsDevelopment())
				{
					services.AddDatabaseDeveloperPageExceptionFilter();
				}
			}
			return services;
		}

		public static IServiceCollection AddAuth<TDbContext, TUser>(
			this IServiceCollection services,
			Action<IdentityOptions> configureIdentityOptions
		)
			where TDbContext : ApiAuthorizationDbContext<TUser>
			where TUser : IdentityUser
		{
			services
				.AddDefaultIdentity<TUser>(configureIdentityOptions)
				.AddEntityFrameworkStores<TDbContext>();

			services
				.AddIdentityServer()
				.AddApiAuthorization<TUser, TDbContext>();

			services
				.AddAuthentication()
				.AddIdentityServerJwt();

			return services;
		}
	}
}
