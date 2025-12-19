using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace M004_Routing.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	private readonly ILogger<WeatherForecastController> _logger;

	public WeatherForecastController(ILogger<WeatherForecastController> logger)
	{
		_logger = logger;
	}

	[HttpGet(Name = "GetWeatherForecast")]
	[Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml, MediaTypeNames.Text.Csv)]
	public IEnumerable<WeatherForecastDTO> Get()
	{
		int zahl = 2143;
		int q = zahl.Quersumme();

		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55)
		}.ToDTO());
	}
}
