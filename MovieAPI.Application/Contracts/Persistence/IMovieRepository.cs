using MovieAPI.Application.Contracts.Enums;
using MovieAPI.Domin.Entities;
using System.Globalization;

namespace MovieAPI.Application.Contracts.Persistence
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMoviesAsync(
            string title,
            Genre? genre,
            SortBy? sortBy,
            int page = 1,
            int pageSize = 10);

    }
}
