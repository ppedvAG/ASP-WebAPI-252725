namespace M004_Routing;

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return (int) x.ToString().Sum(char.GetNumericValue);
	}

	public static IServiceCollection AddDateService(this IServiceCollection services)
	{
		services.AddSingleton<DateService>();
		return services;
	}

	public static WeatherForecastDTO ToDTO(this WeatherForecast w)
	{
		return new WeatherForecastDTO() { Date = w.Date, TemperatureC = w.TemperatureC };
	}

	public static WeatherForecast ToModel(this WeatherForecastDTO dto)
	{
		string[] Summaries = [ "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" ];
		return new WeatherForecast() { Date = dto.Date, TemperatureC = dto.TemperatureC, Summary = Summaries[Random.Shared.Next(Summaries.Length)] };
	}
}