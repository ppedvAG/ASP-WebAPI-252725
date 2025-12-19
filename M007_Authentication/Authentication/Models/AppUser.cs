using Microsoft.AspNetCore.Identity;

namespace M007_Authentication.Authentication.Models;

/// <summary>
/// IdentityUser
/// 
/// Simple Datenklasse, welche quer über AspNetCore.Identity verwendet wird
/// Hat standardmäßig eine GUID als Primary Key
/// </summary>
public class AppUser : IdentityUser { }