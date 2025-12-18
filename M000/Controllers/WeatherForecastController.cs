using M000.Services;
using Microsoft.AspNetCore.Mvc;

namespace M000.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController(ILogger<WeatherForecastController> logger, CounterService counter) : ControllerBase
{
	[Route("GetPrettyDate")]
	public string GetPrettyDate()
	{
		counter.AddEntry("GetPrettyDate");
		return DateTime.Now.ToShortDateString();
	}

	[Route("GetCurrentDate")]
	public DateOnly GetCurrentDate()
	{
		counter.AddEntry("GetCurrentDate");
		return DateOnly.FromDateTime(DateTime.Now);
	}

	[Route("GetCurrentTime")]
	public TimeOnly GetCurrentTime()
	{
		counter.AddEntry("GetCurrentTime");
		return TimeOnly.FromDateTime(DateTime.Now);
	}

	[Route("GetCounterEntries")]
	public Dictionary<string, int> GetCounterEntries()
	{
		return counter.CounterDict;
	}
}
