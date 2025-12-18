namespace M000_MovieStore;

public class MovieService : IMovieService
{
	private List<Movie> _movies = [];

	public MovieService(IEnumerable<Movie> m)
	{
		_movies = m.ToList();
	}

	public IEnumerable<Movie> GetAllMovies()
	{
		return _movies;
	}

	public Movie GetMovieById(long id)
	{
		return _movies.FirstOrDefault(x => x.Id == id);
	}

	public Movie CreateMovie(Movie movie)
	{
		_movies.Add(movie);
		return movie;
	}

	public bool UpdateMovie(long id, Movie movie)
	{
		Movie foundMovie = _movies.FirstOrDefault(e => e.Id == id);
		if (foundMovie != null)
		{
			_movies.Remove(foundMovie);
			_movies.Add(movie);
			return true;
		}
		return false;
	}

	public bool DeleteMovie(long id)
	{
		Movie foundMovie = _movies.FirstOrDefault(e => e.Id == id);
		if (foundMovie != null)
		{
			_movies.Remove(foundMovie);
			return true;
		}
		return false;
	}
}