using M000_MovieStore;

namespace M000;

public static class DTOExtensions
{
	public static MovieDTO ToDTO(this Movie m)
	{
		return new MovieDTO() { Title = m.Title, Description = m.Description, Price = m.Price, Genre = m.Genre };
	}

	public static Movie ToModel(this MovieDTO m)
	{
		return new Movie() { Title = m.Title, Description = m.Description, Price = m.Price, Genre = m.Genre };
	}

	public static IEnumerable<MovieDTO> ToDTOList(this IEnumerable<Movie> list)
	{
		return list.Select(e => e.ToDTO());
	}
}
