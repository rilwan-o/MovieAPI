using Microsoft.OpenApi.Models;
using MovieAPI.Application;
using MovieAPI.Persistence;

namespace MovieAPI.API
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices( this WebApplicationBuilder builder, IWebHostEnvironment env)
        {
            builder.Services.AddApplicationServices();
            builder.Services.AddPersistencServices(builder.Configuration, env);
            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Movie API", Version = "v1" });
            });
            return builder.Build();
        }
    }
}
