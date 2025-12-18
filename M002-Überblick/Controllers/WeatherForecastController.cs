using M002_Überblick.Models;
using M002_Überblick.Services;
using Microsoft.AspNetCore.Mvc;

namespace M002_Überblick.Controllers;


/// <summary>
/// Primary Constructor (ab .NET 8): Kurzform von einem Konstruktor
/// 
/// Erleichert die Dependency Injection stark
/// </summary>

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger) : ControllerBase
{
	/// <summary>
	/// Empfangen von Dependency Injection Objekten über den Konstruktor
	/// 
	/// In Program.cs werden die Objekte registriert
	/// </summary>
	//private readonly ILogger<WeatherForecastController> _logger;

	/// <summary>
	/// Über den Parameter werden die Objekte empfangen
	/// </summary>
	//public WeatherForecastController(ILogger<WeatherForecastController> logger)
	//{
	//	_logger = logger;
	//}

	//////////////////////////////////////////////////////////////////////

	private static readonly string[] Summaries = new[]
	{
		"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
	};

	/// <summary>
	/// Services können anstatt im Konstruktor auch lokal für eine Methode direkt empfangen werden
	/// </summary>
	[HttpGet]
	public IEnumerable<WeatherForecast> Get_CurrentWeather([FromServices] DateService ds)
	{
		logger.Log(LogLevel.Information, "Hallo");
		logger.Log(LogLevel.Information, ds.GetCurrentDate().ToString());

		return Enumerable.Range(1, 5).Select(index => new WeatherForecast
		{
			Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
			TemperatureC = Random.Shared.Next(-20, 55),
			Summary = Summaries[Random.Shared.Next(Summaries.Length)]
		})
		.ToArray();
	}
}
