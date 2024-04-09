using Microsoft.AspNetCore.Mvc;
using MovieAPI.Application.Contracts.Enums;
using MovieAPI.Application.Contracts.Services;
using MovieAPI.Application.Features.Models;
using MovieAPI.Application.Features.ViewModels;

namespace MovieAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
                _movieService = movieService;
        }

        [HttpGet("Search/title/{title}/page/{page}/pageSize/{pageSize}")]
        public async Task<ActionResult<List<MovieVm>>> GetMovies(
            [FromRoute]string title,
            [FromQuery] Genre? genre,
            [FromQuery] SortBy? sortBy,
            [FromRoute] int page = 1,
            [FromRoute] int pageSize = 10
        )
        {
            try
            {
                MovieSearch search = new MovieSearch
                {
                    Title = title,
                    SortBy = sortBy,
                    Genre = genre,
                    Page = page,
                    Size = pageSize
                };
                var movies = await _movieService.Search(search);

                return Ok(movies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
         
        }
    }
}
