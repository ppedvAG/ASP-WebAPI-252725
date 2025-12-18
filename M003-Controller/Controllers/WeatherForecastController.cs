using Microsoft.AspNetCore.Mvc;

namespace M003_Controller.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
	private static readonly string[] Summaries =
	[
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	];

	[HttpGet(Name = "GetWeatherForecast")]
	public IEnumerable<WeatherForecast> Get()
	{
		//Schlecht:
		//List<WeatherForecast> weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		//{
		//	Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
		//	TemperatureC = Random.Shared.Next(-20, 55),
		//	Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		//}).ToList();
		//return weather;

		//Weil: Durch ToList() werden die Elemente iteriert (Schleife)
		//Beim return werden bei der Konvertierung zu JSON nochmal alle Elemente iteriert

		//Gut:
		IEnumerable<WeatherForecast> weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		});
		return weather;
	}

	/// <summary>
	/// IActionResult: Sammlung von Methoden, welche einen HTTP-Status zurückgeben
	/// 
	/// ActionResult: Gibt zusätzlich zum HTTP-Code auch noch Daten mit
	/// </summary>
	[Route("Get2")]
	public ActionResult Get2()
	{
		//if (!string.IsNullOrEmpty(HttpContext.Request.Headers.UserAgent.FirstOrDefault()))
		//{
		//	return BadRequest("Browser nicht erlaubt");
		//}

		IEnumerable<WeatherForecast> weather = Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		});
		return Ok(weather);
	}

	/// <summary>
	/// Wenn in der Query C oder F angegeben werden, werden diese in das Objekt Degrees hineingemappt
	/// </summary>
	[HttpGet]
	[Route("cf")]
	[Produces("application/json")]
	public IEnumerable<WeatherForecast> GetForecastsByCOrF([FromQuery] Degrees cf)
	{
		IEnumerable<WeatherForecast> weather = Enumerable.Range(1, 100).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		});

		return weather.Where(e => e.TemperatureC == cf.C || e.TemperatureF == cf.F);
	}
}

public class Degrees
{
	public int C { get; set; }

	public int F { get; set; }
}