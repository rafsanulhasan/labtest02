
using Fluxor;

using LabTest2.Apps.Web.Server.Data;
using LabTest2.Apps.Web.Server.Models;
using LabTest2.Apps.Web.Shared.Store.Counter;
using LabTest2.Apps.Web.Shared.ViewModels;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace LabTest2.Apps.Web.Server
{
	public class Startup
	{
		private readonly IHostEnvironment _hostEnvironment;

		public Startup(
			IConfiguration configuration,
			IHostEnvironment hostEnvironment
		)
		{
			Configuration = configuration;
			_hostEnvironment = hostEnvironment;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.AddDbContext<ApplicationDbContext>(options =>
					options.UseSqlServer(
						Configuration
							.GetConnectionString("DefaultConnection")
						)
				);

			if (_hostEnvironment.IsDevelopment())
			{
				services.AddDatabaseDeveloperPageExceptionFilter();
			}

			services
				.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
				.AddEntityFrameworkStores<ApplicationDbContext>();

			services
				.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, ApplicationDbContext>();

			services
				.AddAuthentication()
				.AddIdentityServerJwt();

			services.AddControllersWithViews();
			services.AddRazorPages();

			services.AddSyncfusionBlazor();

			services.AddScoped<CounterViewModel>();
			services.AddScoped<FetchDataViewModel>();
			services.AddFluxor(o =>
			{
				if (_hostEnvironment.IsDevelopment())
				{
					o.UseReduxDevTools();
				}
				o.ScanAssemblies(typeof(CounterState).Assembly);
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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

			app.UseHttpsRedirection();
			app.UseBlazorFrameworkFiles();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseIdentityServer();
			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
				endpoints.MapFallbackToFile("index.html");
			});
		}
	}
}
