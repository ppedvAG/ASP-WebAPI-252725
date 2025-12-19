using Microsoft.EntityFrameworkCore;

namespace M000_MovieStore;

public class MovieStoreDbContext : DbContext
{
	public MovieStoreDbContext()
	{
		
	}

	public MovieStoreDbContext(DbContextOptions options) : base(options)
	{

	}

	public DbSet<Movie> Movies { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=MovieStore;Integrated Security=True;Encrypt=False");
	}
}