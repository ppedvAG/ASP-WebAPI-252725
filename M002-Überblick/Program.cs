using M002_Überblick.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services dient zur Registrierung von DI-Objekten
builder.Services.AddSingleton<DateService>();

builder.Services.Configure<DateService>(o => o.ShortDate = true);

WebApplication app = builder.Build();

//////////////////////////////////////////////////////////////////////

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();

	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


/// <summary>
/// Vor langer Zeit: Startup.cs
/// </summary>
class Startup
{
	public static WebApplication ConfigureServices(IServiceCollection services, string[] args)
	{
		WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

		// Add services to the container.

		builder.Services.AddControllers();
		// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
		builder.Services.AddOpenApi();

		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();

		//builder.Services dient zur Registrierung von DI-Objekten
		builder.Services.AddSingleton<DateService>();

		builder.Services.Configure<DateService>(o => o.ShortDate = true);

		return builder.Build();
	}

	public static void ConfigureMiddleware(WebApplication app)
	{
		if (app.Environment.IsDevelopment())
		{
			app.MapOpenApi();

			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();
	}
}