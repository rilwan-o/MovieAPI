using Microsoft.EntityFrameworkCore;
using MovieAPI.Domin.Entities;

namespace MovieAPI.Persistence
{
    public class MovieDbContext : DbContext
    {
        public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }

    }
}
