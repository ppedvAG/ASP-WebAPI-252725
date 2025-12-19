using M007_Authentication.Authentication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace M007_Authentication.Authentication;

/// <summary>
/// NuGet: Microsoft.AspNetCore.Identity.EntityFrameworkCore
/// </summary>
public class AuthDbContext : IdentityDbContext<AppUser>
{
	public AuthDbContext() { }

	public AuthDbContext(DbContextOptions options) : base(options) { }
}
