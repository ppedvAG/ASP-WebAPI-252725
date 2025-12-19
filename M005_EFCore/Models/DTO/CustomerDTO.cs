using System.ComponentModel.DataAnnotations;

namespace M005_EFCore.Models.DTO;

public class CustomerDTO
{
	[Required]
	public string CustomerId { get; set; }

	[Required]
	public string CompanyName { get; set; }
}
