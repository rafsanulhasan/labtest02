using System;
using System.Collections.Generic;
using System.Linq;

using LabTest2.Apps.Web.Shared.DTOs;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LabTest2.Apps.Web.Server.Controllers
{
	[Authorize]
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController
		: ControllerBase
	{
		private static readonly string[] SUMMARIES = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(
			ILogger<WeatherForecastController> logger
		)
			=> _logger = logger;

		[AllowAnonymous]
		[HttpGet]
		public IEnumerable<WeatherForecastDTO> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecastDTO
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = SUMMARIES[rng.Next(SUMMARIES.Length)]
			})
			.ToArray();
		}
	}
}
