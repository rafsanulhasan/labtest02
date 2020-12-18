using Microsoft.Extensions.DependencyInjection;

using NetCore.AutoRegisterDi;

using System;
using System.Linq;
using System.Reflection;

namespace LabTest2.Apps.Web.Shared
{
	public static class ServiceCollectionExtensions
	{
		/// <summary>
		/// Registers classes dynamically by scanning a set of assemlies 
		/// is a specific <see cref="ServiceLifetime">lifetime</see>
		/// </summary>
		/// <param name="services"></param>
		/// <param name="assembliesToScan">Array of assemblies where it should execute scan on</param>
		/// <param name="condition">The condition to filter specific classes</param>
		/// <param name="lifetime">The <see cref="ServiceLifetime">lifetime</see> the service should be registered with</param>
		/// <returns>
		/// </returns>
		public static IServiceCollection Add(
			this IServiceCollection services,
			Assembly[] assembliesToScan,
			Func<Type, bool> condition,
			ServiceLifetime lifetime
		)
		{
			if (assembliesToScan is null || assembliesToScan?.Length == 0)
				throw new ArgumentNullException(nameof(assembliesToScan));
			if (condition is null)
				throw new ArgumentNullException(nameof(condition));

			var assemblyRegistrationData = services
				.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
				.Where(t => condition(t)
					    && t.IsClass
				);

			foreach (var t in assemblyRegistrationData.TypesToConsider)
			{
				if (lifetime == ServiceLifetime.Transient)
					services.AddTransient(t);
				else if (lifetime == ServiceLifetime.Scoped)
					services.AddScoped(t);
				else if (lifetime == ServiceLifetime.Singleton)
					services.AddSingleton(t);
			}

			return services;
		}

		public static IServiceCollection AddAsImplementedAbstraction(
			this IServiceCollection services,
			Assembly[] assembliesToScan,
			Func<Type, bool> condition,
			ServiceLifetime lifetime
		)
		{
			if (assembliesToScan is null || assembliesToScan?.Length == 0)
				throw new ArgumentNullException(nameof(assembliesToScan));
			if (condition is null)
				throw new ArgumentNullException(nameof(condition));

			var assemblyRegistrationData = services
				.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
				.Where(t => condition(t)
					    && t.IsClass
				)
				.AsPublicImplementedInterfaces(lifetime);

			return services;
		}

		public static IServiceCollection AddAsImplementedAbstraction<TIgnoredInterface>(
			this IServiceCollection services,
			Assembly[] assembliesToScan,
			Func<Type, bool> condition,
			ServiceLifetime lifetime,
			bool ignore = true
		)
		{
			if (assembliesToScan is null || assembliesToScan?.Length == 0)
				throw new ArgumentNullException(nameof(assembliesToScan));
			if (condition is null)
				throw new ArgumentNullException(nameof(condition));

			var assemblyRegistrationData = services
				.RegisterAssemblyPublicNonGenericClasses(assembliesToScan)
				.Where(t => condition(t)
					    && t.IsClass
				);

			if (ignore)
				assemblyRegistrationData = assemblyRegistrationData.IgnoreThisInterface<TIgnoredInterface>();

			assemblyRegistrationData.AsPublicImplementedInterfaces(lifetime);

			return services;
		}
	}
}
