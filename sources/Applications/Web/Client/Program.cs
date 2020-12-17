using System;
using System.Net.Http;
using System.Threading.Tasks;

using Fluxor;

using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.Store.FetchData;
using LabTest2.Apps.Web.Shared.ViewModels;

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
			var builder = WebAssemblyHostBuilder
						.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");

			var services = builder.Services;
			var hostEnv = builder.HostEnvironment;

			services
				.AddHttpClient(
					"LabTest2.Apps.Web.ServerAPI",
					client
						=> client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
				)
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
				;

			services
				.AddHttpClient(
					"LabTest2.Apps.Web.ServerAPI-Unrestricted",
					client
						=> client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
				)
				;

			// Supply HttpClient instances that include access tokens when making requests to the server project
			services.AddScoped(sp =>
			{
				var factory = sp.GetRequiredService<IHttpClientFactory>();
				factory.CreateClient("LabTest2.Apps.Web.ServerAPI");
				factory.CreateClient("LabTest2.Apps.Web.ServerAPI-Unrestricted");
				return sp;
			});

			services.AddApiAuthorization();

			services.AddSyncfusionBlazor();

			services.AddFluxor(o =>
			{
				if (hostEnv.IsDevelopment())
				{
					o.UseReduxDevTools();
				}
				o.ScanAssemblies(
					typeof(CounterState).Assembly
				);
			});

			services.AddTransient<CounterViewModel>();
			services.AddTransient<FetchDataViewModel>();

			await builder.Build().RunAsync().ConfigureAwait(false);
		}
	}
}
