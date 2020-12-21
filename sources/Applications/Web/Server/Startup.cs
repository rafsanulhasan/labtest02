
using Fluxor;

using HotChocolate;
using HotChocolate.AspNetCore;
using HotChocolate.AspNetCore.GraphiQL;
using HotChocolate.AspNetCore.Playground;
using HotChocolate.Types;

using LabTest2.Apps.Web.Shared;
using LabTest2.Apps.Web.Shared.GraphTypes;
using LabTest2.Apps.Web.Shared.Services;
using LabTest2.Apps.Web.Client.Store.Counter;
using LabTest2.Data;
using LabTest2.Data.Entities;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.WebSockets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Syncfusion.Blazor;
using Syncfusion.Licensing;

using System;

namespace LabTest2.Apps.Web.Server
{
	public class Startup
	{
		private readonly IHostEnvironment _hostEnvironment;

		private readonly Action<CorsPolicyBuilder> _corsPolicyBuilder;

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment
		)
		{
			Configuration = configuration;
			_hostEnvironment = hostEnvironment;
			_corsPolicyBuilder = b
				=> b.AllowAnyHeader()
				.AllowAnyMethod()
				//.AllowCredentials()
				.AllowAnyOrigin();
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddCors(options 
				=> options.AddDefaultPolicy(_corsPolicyBuilder)
			);

			services.AddDatabaseAndEF<ApplicationDbContext, ApplicationUser>(
				"DefaultConnection",
				typeof(Program).Assembly.FullName
			);

			services.AddAuth<ApplicationDbContext, ApplicationUser>(
				options => options.SignIn.RequireConfirmedAccount = true
			);

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddSyncfusionBlazor();
						
			services.AddFluxor(o =>
			{
				if (_hostEnvironment.IsDevelopment())
				{
					o.UseReduxDevTools();
				}
				o.ScanAssemblies(typeof(CounterState).Assembly);
			});

			var assembliesToScan = new[]
			{
				typeof(IWeatherForecastService).Assembly
			};

			services.AddAsImplementedAbstraction(
				assembliesToScan,
				t => t.Name.EndsWith("Service"),
				ServiceLifetime.Singleton
			);

			services
				.AddGraphQLServer()
				.AddQueryType<Query>()
				.AddMutationType<Mutation>()
				.AddSubscriptionType<Subscription>()
				.AddInMemorySubscriptions()
				.BindRuntimeType<string, StringType>()
				.BindRuntimeType<Guid, IdType>()
				.AddDataLoader<WeatherForecastDataLoader>()
				;

			services.AddWebSockets(opt=> { });
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			SyncfusionLicenseProvider.RegisterLicense(Configuration.GetValue<string>("SyncFusion:License"));
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseMigrationsEndPoint();
				app.UseWebAssemblyDebugging();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}
			app.UseCors(_corsPolicyBuilder);

			var queryPath = "/graph";
			var subscriptionPath = $"{queryPath}/subscriptions";
			var playgroundPath = $"{queryPath}/play";
			var graphiqlPath = $"{queryPath}/graphiql";
			
			app
				.UsePlayground(new PlaygroundOptions()
				{
					EnableSubscription = true,
					Path = playgroundPath,
					QueryPath = queryPath,
					SubscriptionPath = queryPath
				})
				.UseGraphiQL(new GraphiQLOptions
				{
					EnableSubscription = true,
					Path = graphiqlPath,
					QueryPath = queryPath,
					SubscriptionPath = queryPath
				});
			app.UseWebSockets();

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAAAWithIdentityServer();

			app.UseEndpoints(endpoints =>
			{
				endpoints
					 .MapGraphQL(queryPath)
					 .AllowAnonymous()
					 .RequireCors(_corsPolicyBuilder)
					 .WithOptions(
						new GraphQLServerOptions
						{
							EnableSchemaRequests = true,
							AllowedGetOperations = AllowedGetOperations.QueryAndMutation,
							EnableGetRequests = true
						}
					 );
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}

