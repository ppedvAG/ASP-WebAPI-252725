using Microsoft.AspNetCore.Mvc;

namespace M006_Testing.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
	private static readonly string[] Summaries = [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" ];

	[HttpGet(Name = "GetWeatherForecast")]
	[Route("5days")]
	public IEnumerable<WeatherForecast> Get()
	{
		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		});
	}

	[HttpGet]
	[Route("AmountSummaries")]
	public int GetSummaryAmount()
	{
		return Summaries.Length;
	}

	[HttpPost]
	public double SumTwoNumbers(double x, double y)
	{
		return x + y;
	}
}
