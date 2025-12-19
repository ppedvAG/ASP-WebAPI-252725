using M000_MovieStore;
using Microsoft.AspNetCore.Mvc;

namespace M000.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieStoreController(IMovieService ms) : ControllerBase
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
		MovieStoreDbContext db = new();
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
}

public class SearchParams
{
	public DateTime PublishDate { get; set; }

	public MovieGenre Genre { get; set; }

	public decimal MinPrice { get; set; }

	public decimal MaxPrice { get; set; }
}