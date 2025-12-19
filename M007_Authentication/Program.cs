using M007_Authentication;
using M007_Authentication.Authentication;
using M007_Authentication.Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//////////////////////////////////////////////////////////////////////////////

builder.Services.AddDbContext<AuthDbContext>(o => o.UseSqlServer("Data Source=localhost;Initial Catalog=AuthDB;Integrated Security=True;Encrypt=False"));

builder.Services.AddIdentity<AppUser, IdentityRole>(o =>
{
	o.Password.RequireNonAlphanumeric = false;
	o.Password.RequireDigit = false;
	o.Password.RequireLowercase = false;
	o.Password.RequireUppercase = false;
	o.Password.RequiredLength = 3;
})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<AuthDbContext>(); //Verwende den gegebenen DbContext für die Auth-DB

//////////////////////////////////////////////////////////////////////////////

builder.Services.AddTransient<ITokenService, JwtTokenService>();

string unencryptedKey = "dasisteinsehrlangerschlüsseldereigentlichineinerdateigespeichertwerdensollte";
SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(unencryptedKey));

builder.Services.AddAuthentication(o => //Für alle Authentication Bearer Tokens verwenden
{
	o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
	o.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(o =>
{
	o.TokenValidationParameters = new TokenValidationParameters()
	{
		IssuerSigningKey = key,
		ValidAudience = "example.com",
		ValidIssuer = "example.com",

		ValidateIssuerSigningKey = true,
		ValidateAudience = true,
		ValidateIssuer = true
	};
}); //NuGet: Microsoft.AspNetCore.Authentication.JwtBearer

//////////////////////////////////////////////////////////////////////////////

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
