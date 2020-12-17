using Microsoft.AspNetCore.Builder;

namespace LabTest2.Data
{
	public static class ApplicationBuilderExtensions
	{
		public static void UseAAAWithIdentityServer(
			this IApplicationBuilder app
		)
		{
			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
