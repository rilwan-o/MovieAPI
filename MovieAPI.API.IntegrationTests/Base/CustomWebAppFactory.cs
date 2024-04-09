using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieAPI.Persistence;

namespace MovieAPI.API.IntegrationTests.Base
{
    public class CustomWebAppFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");
            builder.ConfigureServices(services =>
            {
                var context = services.FirstOrDefault(descriptor => descriptor.ServiceType == typeof(MovieDbContext));
                if(context != null)
                {
                    services.Remove(context);
                    var options = services.Where(r => r.ServiceType == typeof(MovieDbContext)
                    || r.ServiceType.IsGenericType && r.ServiceType.GetGenericTypeDefinition() == typeof(DbContextOptions<>)).ToArray();
                    foreach(var option in options)
                    {
                        services.Remove(option);
                    }
                }

                services.AddDbContext<MovieDbContext>(options =>
                {
                    options.UseInMemoryDatabase("Movie");
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var dbContext = scopedServices.GetRequiredService<MovieDbContext>();
                    var logger = scopedServices.GetRequiredService<ILogger<CustomWebAppFactory<TStartup>>>();
                    dbContext.Database.EnsureCreated();

                    try
                    {
                        new Utilities().InitializeDbForTests(dbContext);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occurred seeding the database" +
                            $" with test data. Error: {ex.Message}");
                    }
                }
            });
        }
    }
}
