using GreenDonut;

using HotChocolate.DataLoader;

using LabTest2.Apps.Web.Shared.DTOs;
using LabTest2.Apps.Web.Shared.Services;

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LabTest2.Apps.Web.Shared.GraphTypes
{	
	public class WeatherForecastDataLoader
		: DataLoaderBase<DateTime, WeatherForecastDTO>, IDataLoader<DateTime, WeatherForecastDTO>
	{
		private readonly IWeatherForecastService _forecastService;

		public WeatherForecastDataLoader(
		  IBatchScheduler batchScheduler,
		  IWeatherForecastService forecastService
		)
		  : base(batchScheduler)
			=> _forecastService = forecastService;

		protected override async ValueTask<IReadOnlyList<Result<WeatherForecastDTO>>> FetchAsync(
			IReadOnlyList<DateTime> keys,
			CancellationToken cancellationToken
		)
		{
			IReadOnlyList<Result<WeatherForecastDTO>> result;
			try
			{
				var forecasts = _forecastService.GetForecasts();
				if (keys is not null && keys.Count > 0)
					forecasts = forecasts.Where(f => keys.Contains(f.Date));

				result = await forecasts
						.Select(f => Result<WeatherForecastDTO>.Resolve(f))
						.ToListAsync(cancellationToken);
			} 
			catch (Exception ex)
			{
				result = new List<Result<WeatherForecastDTO>>
				{
					Result<WeatherForecastDTO>.Reject(ex)
				};
			}
			return result;
		}
	}
}
