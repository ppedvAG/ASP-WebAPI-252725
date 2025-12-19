using M005_EFCore.Models;
using M005_EFCore.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace M005_EFCore.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NorthwindController(NorthwindContext db) : ControllerBase
{
	[HttpGet]
	[Route("allcustomers")]
	public IEnumerable<Customers> GetAllCustomers()
	{
		return db.Customers;
	}

	[HttpPost]
	[Route("newcustomer")]
	public ActionResult PostNewCustomer([FromQuery] CustomerDTO c)
	{
		Customers c2 = new Customers();
		c2.CustomerId = c.CustomerId;
		c2.CompanyName = c.CompanyName;

		try
		{
			db.Customers.Add(c2);
			db.SaveChanges();
		}
		catch (Exception)
		{
			return BadRequest();
		}
		return Ok();
	}
}
