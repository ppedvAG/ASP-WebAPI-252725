using M000_MovieStore;
using Microsoft.AspNetCore.Mvc;

namespace M000.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MovieStoreController(IMovieService ms) : ControllerBase
{
	[HttpGet]
	[Route("AllMovies")]
	public IEnumerable<Movie> GetAllMovies()
	{
		return ms.GetAllMovies();
	}

	[HttpPost]
	[Route("NewMovie")]
	public void PostNewMovie(Movie m)
	{
		ms.CreateMovie(m);
	}
}