using AutoMapper;
using Humanizer;
using MovieAPI.Application.Contracts.Enums;
using MovieAPI.Application.Contracts.Persistence;
using MovieAPI.Application.Contracts.Services;
using MovieAPI.Application.Features.Models;
using MovieAPI.Application.Features.ViewModels;
using MovieAPI.Application.profiles;
using MovieAPI.Application.UnitTests.Mocks;
using Shouldly;

namespace MovieAPI.Application.UnitTests.Contracts.Services
{
    public class MovieServiceTests
    {
        private readonly IMapper _mapper;
        private readonly IMovieRepository _mockMovieRepository;
        private  readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _mockMovieRepository = new RepositoryMocks().GetMovieRepository();
            var configurationProvider = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
            _movieService = new MovieService(_mockMovieRepository, _mapper);
        }

        [Fact]
        public async Task Search_ByTitle_ReturnsFilteredMovies()
        {  
            var movieSearch = new MovieSearch { Title = "Bat", Page=1, Size = 5 };
            // Act
            var result = await _movieService.Search(movieSearch);

            // Assert
            result.ShouldBeOfType<List<MovieVm>>();
            foreach (var item in result)
            {
                item.Title.ShouldContain(movieSearch.Title);
            }
        }

        [Fact]
        public async Task Search_LimitsResultsPerPage()
        {
            var movieSearch = new MovieSearch { Title = "Bat", Page = 1, Size = 5 };
            // Act
            var result = await _movieService.Search(movieSearch);

            // Assert
            result.ShouldBeOfType<List<MovieVm>>();
            result.Count().ShouldBeLessThanOrEqualTo(5);
        }

        [Fact]
        public async Task Search_FiltersMoviesByGenre()
        {
            var movieSearch = new MovieSearch { Title = "Bat", Genre = Genre.Action, Page = 1, Size = 5 };
            // Act
            var result = await _movieService.Search(movieSearch);

            // Assert
            result.ShouldBeOfType<List<MovieVm>>();
            result.Count().ShouldBeLessThanOrEqualTo(5);
            foreach (var item in result)
            {
                item.Genre.ShouldContain(movieSearch.Genre.ToString().Humanize());
            }
        }

        [Fact]
        public async Task Search_SortsMoviesByTitle()
        {
            var movieSearch = new MovieSearch { Title = "Bat", SortBy = SortBy.Title, Page = 1, Size = 5 };
            // Act
            var result = await _movieService.Search(movieSearch);
            var movieSearch2 = new MovieSearch { Title = "Bat", Page = 1, Size = 5 };
            var result2 = await _movieService.Search(movieSearch);
            var result3 = result2.OrderBy(m => m.Title);
            // Assert
            result.ShouldBeOfType<List<MovieVm>>();
            for(int i=0; i<result.Count(); i++)    
            {
                result.ToList()[i].Title.ShouldBeEquivalentTo(result3.ToList()[i].Title);
            }
        }

        [Fact]
        public async Task Search_SortsMoviesByReleaseDate()
        {
            var movieSearch = new MovieSearch { Title = "Bat", SortBy = SortBy.ReleaseDate, Page = 1, Size = 5 };
            // Act
            var result = await _movieService.Search(movieSearch);
            var movieSearch2 = new MovieSearch { Title = "Bat", Page = 1, Size = 5 };
            var result2 = await _movieService.Search(movieSearch);
            var result3 = result2.OrderBy(m => m.ReleaseDate);
            // Assert
            result.ShouldBeOfType<List<MovieVm>>();
            for (int i = 0; i < result.Count(); i++)
            {
                result.ToList()[i].Title.ShouldBeEquivalentTo(result3.ToList()[i].Title);
            }
        }
    }
}
