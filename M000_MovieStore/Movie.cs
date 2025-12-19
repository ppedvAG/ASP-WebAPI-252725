using System.ComponentModel.DataAnnotations;

namespace M000_MovieStore;

public class Movie
{
	[Key]
	public long Id { get; set; }

	[StringLength(100)]
	[Required]
	public string Title { get; set; }

	[StringLength(100)]
	public string Description { get; set; }

	[Range(0, 100)]
	public decimal Price { get; set; }

	public DateTime? PublishedDate { get; set; }

	public MovieGenre Genre { get; set; }

	[StringLength(500)]
	public string? ImageUrl { get; set; }
}
