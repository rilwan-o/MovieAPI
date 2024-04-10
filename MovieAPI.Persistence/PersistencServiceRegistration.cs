using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Application.Contracts.Persistence;
using MovieAPI.Domin.Entities;
using MovieAPI.Persistence.Data;
using MovieAPI.Persistence.Repositories;

namespace MovieAPI.Persistence
{
    public static class PersistencServiceRegistration
    {
        public static IServiceCollection AddPersistencServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            var serviceProvider = services.AddDbContext<MovieDbContext>(options =>
            {
                var connection = configuration.GetConnectionString("MovieAPIConnectionString");
                if (env == null || env.EnvironmentName == "Test")
                {
                    options.UseSqlite(connection);
                }
                else
                {
                    options.UseSqlServer(connection);
                }

            }).BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

                try
                {
                    dbContext.Database.Migrate();

                    Console.WriteLine("Migrations applied successfully.");
                    List<Movie> excelData = new List<Movie>();
                    try
                    {
                        if (!dbContext.Movies.Any())
                        {
                            var filePath = ConfigurationHelper.Configuration["movieData"];
                            excelData = CsvFileReader.ReadCsvData(filePath);
                            // Seed the database                     
                            foreach (var data in excelData)
                            {
                                dbContext.Movies.Add(data);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"No csv data found: {ex.Message}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error applying migrations: {ex.Message}");
                    throw;
                }
            }

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<IMovieRepository, MovieRepository>();

            return services;
        }
    }
}
