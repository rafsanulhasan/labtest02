using Fluxor;

using LabTest2.Apps.Web.Client.GraphClient;
using LabTest2.Apps.Web.Shared;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

using StrawberryShake.Transport.WebSockets;

using Syncfusion.Blazor;

using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

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
			var baseAddress = builder.HostEnvironment.BaseAddress;
			var websocketAddress = baseAddress
								.Replace("https", "wss")
								.Concat("graph")
								.ToString();
			Console.WriteLine(websocketAddress);

			services
				.AddHttpClient(
					"LabTest2.Apps.Web.ServerAPI",
					client
						=> client.BaseAddress = new Uri(baseAddress)
				)
				.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>()
				;

			services
				.AddHttpClient(
					"LabTest2.Apps.Web.ServerAPI-Unrestricted",
					client =>
					{
						client.BaseAddress = new Uri(baseAddress);
						client.DefaultRequestVersion = Version.Parse("2.0");
					}
				)
				;

			services
				.AddHttpClient(
					"LabTest2Client",
					client =>
					{
						client.BaseAddress = new Uri("https://localhost:5001/graph");
						client.DefaultRequestVersion = Version.Parse("2.0");
					}
				);

			services.AddWebSocketClient(
			    "LabTest2Client",
			    c => c.Uri = new Uri("wss://localhost:5001/graph")
			);

			services.AddLabTest2Client();

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

			var assembliesToScan = new[]
			{
				typeof(CounterViewModel).Assembly
			};

			services.AddFluxor(o =>
			{
				if (hostEnv.IsDevelopment())
					o.UseReduxDevTools();
				else
					o.UseRouting();

				o.ScanAssemblies(
					assembliesToScan.First()
				);
			});

			services.Add(
				assembliesToScan,
				t => t.Name.EndsWith("ViewModel"),
				ServiceLifetime.Transient
			);
			
			await builder.Build().RunAsync().ConfigureAwait(false);
		}
	}
}
