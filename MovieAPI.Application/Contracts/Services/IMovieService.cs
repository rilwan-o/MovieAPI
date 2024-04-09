using MovieAPI.Application.Features.Models;
using MovieAPI.Application.Features.ViewModels;

namespace MovieAPI.Application.Contracts.Services
{
    public  interface IMovieService
    {
        Task<IEnumerable<MovieVm>> Search(MovieSearch movieSearch);
    }

}
