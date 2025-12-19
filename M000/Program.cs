using M000.Services;
using M000_MovieStore;
using Microsoft.EntityFrameworkCore;
using OpenTelemetry.Logs;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
	.AddJsonOptions(o => o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.AddDbContext<MovieStoreDbContext>(o => o.UseSqlServer("Data Source=localhost;Initial Catalog=MovieStore;Integrated Security=True;Encrypt=False"));

builder.Logging.AddOpenTelemetry(o => o.AddConsoleExporter());

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<CounterService>();

//Wenn in einem Controller IMovieService verwendet wird, kann hier definiert werden, um welches konkrete Objekt es sich handelt
builder.Services.AddSingleton<IMovieService, MovieService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
