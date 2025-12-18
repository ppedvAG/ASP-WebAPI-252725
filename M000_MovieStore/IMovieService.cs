namespace M000_MovieStore;

public interface IMovieService
{
	IEnumerable<Movie> GetAllMovies();

	Movie GetMovieById(long id);

	Movie CreateMovie(Movie movie);

	bool UpdateMovie(long id, Movie movie);

	bool DeleteMovie(long id);
}