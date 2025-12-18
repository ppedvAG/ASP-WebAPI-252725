namespace M000_MovieStore;

public class Movie
{
	public long Id { get; set; }

	public string Title { get; set; }

	public string Description { get; set; }

	public decimal Price { get; set; }

	public DateTime? PublishedDate { get; set; }

	public MovieGenre Genre { get; set; }

	public string? ImageUrl { get; set; }
}
