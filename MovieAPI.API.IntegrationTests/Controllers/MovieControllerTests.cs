using MovieAPI.API.IntegrationTests.Base;
using MovieAPI.Application.Features.ViewModels;
using System.Text.Json;

namespace MovieAPI.API.IntegrationTests.Controllers
{
    public class MovieControllerTests : IClassFixture<CustomWebAppFactory<Program>>
    {
        private HttpClient _client;
        private readonly CustomWebAppFactory<Program> _factory;
        public MovieControllerTests(CustomWebAppFactory<Program> factory) 
        { 
            _factory = factory;
        }

        [Fact]
        public async Task ReturnsSuccessResult()
        {
            _client = _factory.CreateClient();

            var response = await _client.GetAsync("api/Movie/Search/title/bat/page/1/pageSize/10");
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<MovieVm>>(responseString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.IsType<List<MovieVm>>(result);   
        }
    }
}
