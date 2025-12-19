using Bogus;
using M000_MovieStore;
using Microsoft.AspNetCore.Mvc;

namespace M000.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieStoreController(IMovieService ms, MovieStoreDbContext db) : ControllerBase
{
	[HttpGet]
	[Route("AllMovies")]
	public ActionResult<IEnumerable<MovieDTO>> GetAllMovies()
	{
		IEnumerable<MovieDTO> movies = ms.GetAllMovies().Select(e => e.ToDTO()); //Select: Transformation nach einem Schema
		if (!movies.Any())
			return NoContent();

		return Ok(ms.GetAllMovies());
	}

	[HttpPost]
	[Route("NewMovie")]
	public void PostNewMovie(MovieDTO m)
	{
		db.Movies.Add(new Movie() { Id = 0, Title = "Pulp Fiction", Price = 10, Description = "Ein cooler Film", Genre = MovieGenre.Action, PublishedDate = DateTime.Now });
		db.SaveChanges();
		//ms.CreateMovie(m.ToModel());
	}

	[HttpGet]
	[Route("Filter")]
	public IEnumerable<MovieDTO> GetMoviesByFilters([FromQuery] SearchParams sp)
	{
		IEnumerable<Movie> m = ms.GetAllMovies();
		IEnumerable<Movie> filtered = m.Where(e => (e.Price > sp.MinPrice && e.Price < sp.MaxPrice) || e.PublishedDate == sp.PublishDate || e.Genre == sp.Genre);
		return filtered.ToDTOList();
	}

	[HttpPost]
	[Route("Generate")]
	public ActionResult Generate100Movies()
	{
		Faker<Movie> f = new Faker<Movie>()
			.RuleFor(e => e.Title, e => e.Lorem.Word())
			.RuleFor(e => e.Description, e => new string(e.Lorem.Text().Take(99).ToArray()))
			.RuleFor(e => e.Price, e => e.Random.Int(300, 1000) / 100.0m)
			.RuleFor(e => e.PublishedDate, e => e.Date.Between(new DateTime(1980, 1, 1), new DateTime(2026, 1, 1)))
			.RuleFor(e => e.Genre, e => e.PickRandom<MovieGenre>())
			.RuleFor(e => e.ImageUrl, e => e.Internet.UrlWithPath());

		List<Movie> movies = f.Generate(100);

		try
		{
			db.Movies.AddRange(movies);
			db.SaveChanges();
		}
		catch (Exception)
		{
			return StatusCode(500);
		}

		return Ok();
	}
}

public class SearchParams
{
	public DateTime PublishDate { get; set; }

	public MovieGenre Genre { get; set; }

	public decimal MinPrice { get; set; }

	public decimal MaxPrice { get; set; }
}