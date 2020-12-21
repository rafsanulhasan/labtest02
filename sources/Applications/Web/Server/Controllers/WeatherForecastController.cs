using LabTest2.Apps.Web.Shared.DTOs;
using LabTest2.Apps.Web.Shared.Services;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace LabTest2.Apps.Web.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController
		: ControllerBase
	{
		private readonly ILogger<WeatherForecastController> _logger;
		private readonly IWeatherForecastService _forecastService;

		public WeatherForecastController(
			ILogger<WeatherForecastController> logger,
			IWeatherForecastService forecastService
		)
		{
			_logger = logger;
			_forecastService = forecastService;
		}

		[AllowAnonymous]
		[HttpGet]
		public IAsyncEnumerable<WeatherForecastDTO> Get() 
			=> _forecastService.GetForecasts();
	}
}
