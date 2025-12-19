using M007_Authentication.Authentication;
using M007_Authentication.Authentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace M007_Authentication.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, ITokenService jwt) : ControllerBase
{
	[HttpPost]
	[Route("CreateRoles")]
	public async void CreateRoles()
	{
		IdentityRole adminRole = new IdentityRole("Admin");
		IdentityRole userRole = new IdentityRole("User");

		await roleManager.CreateAsync(adminRole);
		await roleManager.CreateAsync(userRole);
	}

	[HttpPost]
	[Route("CreateUser")]
	public async Task<ActionResult> CreateUser([FromQuery] string userName, [FromQuery] string password)
	{
		AppUser user = new AppUser() { UserName = userName };

		AppUser? foundUser = await userManager.FindByNameAsync(userName);
		if (foundUser != null)
		{
			await userManager.DeleteAsync(foundUser);
			//return BadRequest();
		}

		IdentityResult result = await userManager.CreateAsync(user, password);
		if (result.Succeeded)
		{
			IdentityRole adminRole = new IdentityRole("Admin");
			await roleManager.CreateAsync(adminRole);
			await userManager.AddToRoleAsync(user, adminRole.Name);
			return Ok();
		}

		return BadRequest();
	}

	[HttpPost]
	[Route("LoginUser")]
	public async Task<ActionResult> LoginUser([FromQuery] string userName, [FromQuery] string password)
	{
		AppUser foundUser = await userManager.FindByNameAsync(userName);
		if (foundUser == null)
			return BadRequest();

		Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.CheckPasswordSignInAsync(foundUser, password, false);
		if (result.Succeeded)
		{
			string token = jwt.CreateToken(foundUser);
			return Ok(token);
		}
		return BadRequest();
	}
}