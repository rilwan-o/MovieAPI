using Microsoft.EntityFrameworkCore;
using MovieAPI.Domin.Entities;
using MovieAPI.Persistence;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieAPI.API.IntegrationTests.Base
{
    public class Utilities
    {
        public void InitializeDbForTests(MovieDbContext context)
        {
            var movies = new List<Movie>
            {
                new Movie
                {
                    Title = "The Batman",
                    Genre = "Crime, Mystery, Thriller",
                    Overview = "In his second year of fighting crime, Batman uncovers ...",
                    Popularity = 3827.658m,
                    OriginalLanguage = "en",
                    VoteAverage = 8.1m,
                    VoteCount = 1151,
                    ReleaseDate = DateTime.ParseExact("15/12/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PosterUrl = "https://image.tmdb.org/t/p/original/1g0dhYtq4irTY1GPXvft6k4YLjm.jpg"
                },
                 new Movie
                {
                    Title = "Mortal Kombat",
                    Genre = "Action, Fantasy, Adventure",
                    Overview = "Washed-up MMA fighter Cole Young, unaware",
                    Popularity = 0.1m,
                    OriginalLanguage = "en",
                    VoteAverage = 1.0m,
                    VoteCount = 1,
                    ReleaseDate = DateTime.ParseExact("15/12/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PosterUrl = ""
                },
                  new Movie
                {
                    Title = "Batman",
                    Genre = "Fantasy, Action",
                    Overview = "Batman must face his most ruthless nemesis ",
                    Popularity = 0.1m,
                    OriginalLanguage = "en",
                    VoteAverage = 1.0m,
                    VoteCount = 1,
                    ReleaseDate = DateTime.ParseExact("20/12/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PosterUrl = ""
                },
                   new Movie
                {
                    Title = "Batman v Superman: Dawn of Justice",
                    Genre = "Action, Adventure, Fantasy",
                    Overview = "",
                    Popularity = 0.1m,
                    OriginalLanguage = "en",
                    VoteAverage = 1.0m,
                    VoteCount = 1,
                    ReleaseDate = DateTime.ParseExact("20/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PosterUrl = ""
                },
                    new Movie
                {
                    Title = "Die Hard Begins",
                    Genre = "Action, Crime, Drama",
                    Overview = "Driven by tragedy, billionaire Bruce Wayne",
                    Popularity = 0.1m,
                    OriginalLanguage = "en",
                    VoteAverage = 1.0m,
                    VoteCount = 1,
                    ReleaseDate = DateTime.ParseExact("20/12/2021", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    PosterUrl = ""
                    }
            };

            context.Movies.AddRange(movies);
            context.SaveChanges();
        }
    }
}
