using System;
using System.Net.Http;
using System.Threading.Tasks;

using Fluxor;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using Syncfusion.Blazor;

namespace LabTest2.Apps.Web.Client
{
	public static class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			builder.Services.AddHttpClient("LabTest2.Apps.Web.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
			    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

			// Supply HttpClient instances that include access tokens when making requests to the server project
			builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("LabTest2.Apps.Web.ServerAPI"));

			builder.Services.AddApiAuthorization();

			builder.Services.AddSyncfusionBlazor();

			builder.Services.AddFluxor(o => o.ScanAssemblies(typeof(Program).Assembly));

			await builder.Build().RunAsync().ConfigureAwait(false);
		}
	}
}
