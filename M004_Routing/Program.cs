using M004_Routing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(o => o.OutputFormatters.Add(new CsvFormatter())).AddXmlSerializerFormatters();

//builder.Services.AddSingleton<DateService>();
builder.Services.AddDateService(); //Nach der Erweiterungsmethode

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllerRoute("default", "{controller}/api/{action}");

app.Run();
