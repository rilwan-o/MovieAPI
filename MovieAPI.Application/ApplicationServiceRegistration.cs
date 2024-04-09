using Microsoft.Extensions.DependencyInjection;
using MovieAPI.Application.Contracts.Services;

namespace MovieAPI.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient<IMovieService, MovieService>();

            return services;
        }
    }
}
