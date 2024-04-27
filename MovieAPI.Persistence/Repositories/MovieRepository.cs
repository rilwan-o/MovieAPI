using Humanizer;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Application.Contracts.Enums;
using MovieAPI.Application.Contracts.Persistence;
using MovieAPI.Domin.Entities;

namespace MovieAPI.Persistence.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieDbContext dbContext) : base(dbContext)
        {      
        }

        public async Task<IEnumerable<Movie>> GetMoviesAsync(
            string title,
            Genre? genre,
            SortBy? sortBy,
            int page = 1, 
            int pageSize = 10)
        {
            // provides better performance and query optimization capabilities through deferred execution. No need for keeping data in memory
            var query = _dbContext.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(title))
            {
                //DRY
                query = SearchByTitle(title, query);
            }
            
            if (genre != null)
            {
                var movieGenre = genre.Humanize();
                query = SearchByGenre(query, movieGenre);
            }

            if (sortBy != null)
            {
                if (sortBy == SortBy.Title)
                    query = SortByTitle(query);
                if (sortBy == SortBy.ReleaseDate)
                    query = SortByReleaseDate(query);
            }
            List<Movie> movies = await PaginateMovies(page, pageSize, query);

            return movies;
        }

        private static async Task<List<Movie>> PaginateMovies(int page, int pageSize, IQueryable<Movie> query)
        {
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        private static IQueryable<Movie> SearchByTitle(string title, IQueryable<Movie> query)
        {
            query = query.Where(m => m.Title.ToLower().Contains(title.ToLower()));
            return query;
        }

        private static IQueryable<Movie> SearchByGenre(IQueryable<Movie> query, string movieGenre)
        {
            query = query.Where(m => m.Genre.ToLower().Contains(movieGenre.ToLower()));
            return query;
        }

        private static IQueryable<Movie> SortByTitle(IQueryable<Movie> query)
        {
            query = query.OrderBy(m => m.Title);
            return query;
        }

        private static IQueryable<Movie> SortByReleaseDate(IQueryable<Movie> query)
        {
            query = query.OrderBy(m => m.ReleaseDate);
            return query;
        }
    }
}
