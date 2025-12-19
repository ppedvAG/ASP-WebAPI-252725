using M006_Testing;
using M006_Testing.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace M006_Testing_Tests;

public class WeatherForecastTests : IDisposable
{
	private WeatherForecastController controller;

	/// <summary>
	/// Setup-Code
	/// </summary>
	public WeatherForecastTests()
	{
		controller = new WeatherForecastController(null);
	}

	/// <summary>
	/// Cleanup-Code
	/// </summary>
	public void Dispose()
	{

	}

	////////////////////////////////////////////////////////////////

	[Fact]
	public void CheckForFiveResults()
	{
		//AAA

		//Arrange

		//Act
		IEnumerable<WeatherForecast> wf = controller.Get();

		//Assert
		Assert.Equal(5, wf.Count());
	}

	[Fact]
	public void CheckForTemperatureRange()
	{
		//AAA

		//Arrange

		//Act
		IEnumerable<WeatherForecast> wf = controller.Get();

		//Assert
		bool min = wf.Min(e => e.TemperatureC) >= -20;
		bool max = wf.Max(e => e.TemperatureC) <= 54;

		Assert.True(min && max);
	}

	[Fact]
	public void CheckForAmountSummaries()
	{
		int x = controller.GetSummaryAmount();
		Assert.Equal(10, x);
	}

	[Theory]
	[InlineData(3, 4, 7)]
	[InlineData(7, 1, 8)]
	[InlineData(9, 9, 18)]
	public void CheckForSums(double x, double y, double erg)
	{
		double ergebnis = controller.SumTwoNumbers(x, y);

		Assert.Equal(ergebnis, erg);
	}

	[Fact]
	public async void LiveTest()
	{
		using HttpClient client = new HttpClient();

		HttpResponseMessage resp = await client.GetAsync("localhost:..../...");

		if (!resp.IsSuccessStatusCode)
			Assert.Fail();

		string ergebnis = await resp.Content.ReadAsStringAsync();

		IEnumerable<WeatherForecast> wf = JsonSerializer.Deserialize<IEnumerable<WeatherForecast>>(ergebnis);

		Assert.Equal(5, wf.Count());
	}
}
