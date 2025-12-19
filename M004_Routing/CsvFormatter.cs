using Microsoft.AspNetCore.Mvc.Formatters;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace M004_Routing;

public class CsvFormatter : TextOutputFormatter
{
	/// <summary>
	/// Hier werden statische Einstellungen festgelegt, die jeder Formatter haben muss
	/// </summary>
	public CsvFormatter()
	{
		SupportedMediaTypes.Add(MediaTypeNames.Text.Csv); //Welches Format wird hier unterstützt
		SupportedEncodings.Add(Encoding.UTF8); //Welches Encoding wird hier unterstützt
	}

	public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
	{
		HttpResponse response = context.HttpContext.Response;

		List<string> allRows = [];
		if (context.Object is IEnumerable<object> data) //Direkter Cast bei einem Typvergleich mit is
		{
			//IEnumerable<object> data = (IEnumerable<object>) context.Object; //Selbiges wie in der if-Bedingung
			
			foreach (object item in data) //Objekte von der Response einzeln durchgehen
			{
				List<object> fields = [];
				foreach (PropertyInfo pi in item.GetType().GetProperties())
				{
					fields.Add(pi.GetValue(item));
				}
				allRows.Add(string.Join(";", fields));
			}
		}

		await response.WriteAsync(string.Join("\n", allRows));
	}
}
/*
[
  {
    "date": "2025-12-20",
    "temperatureC": -10,
    "temperatureF": 15,
    "summary": "Warm"
  },
  {
    "date": "2025-12-21",
    "temperatureC": 16,
    "temperatureF": 60,
    "summary": "Hot"
  },
  {
    "date": "2025-12-22",
    "temperatureC": 53,
    "temperatureF": 127,
    "summary": "Chilly"
  },
  {
    "date": "2025-12-23",
    "temperatureC": 40,
    "temperatureF": 103,
    "summary": "Warm"
  },
  {
    "date": "2025-12-24",
    "temperatureC": 18,
    "temperatureF": 64,
    "summary": "Scorching"
  }
]
*/