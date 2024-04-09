using AutoMapper;
using MovieAPI.Application.Contracts.Persistence;
using MovieAPI.Application.Exceptions;
using MovieAPI.Application.Features.Models;
using MovieAPI.Application.Features.ViewModels;
using MovieAPI.Application.Validations;
using MovieAPI.Domin.Entities;

namespace MovieAPI.Application.Contracts.Services
{
    public class MovieService : IMovieService
    {
        public readonly IMovieRepository _movieRepository;
        private readonly IMapper _mapper;
        public MovieService(IMovieRepository movieRepository, IMapper mapper)
        {
            _movieRepository = movieRepository;   
            _mapper = mapper;
        }
        public async Task<IEnumerable<MovieVm>> Search(MovieSearch movieSearch)
        {
            var validator = new MovieSearchValidator();
            var validationResult = await validator.ValidateAsync(movieSearch);

            if (!validationResult.IsValid)
            {
                throw new MovieSearchException(validationResult.Errors);
            }

            var movies = await _movieRepository.GetMoviesAsync(
                movieSearch.Title,
                movieSearch.Genre,
                movieSearch.SortBy,
                movieSearch.Page,
                movieSearch.Size);

            var moviesVm = _mapper.Map<List<Movie>, List<MovieVm>>(movies.ToList());

            return moviesVm;
        }
    }
}
