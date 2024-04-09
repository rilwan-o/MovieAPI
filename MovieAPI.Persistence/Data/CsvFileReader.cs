using CsvHelper;
using CsvHelper.Configuration;
using MovieAPI.Domin.Entities;
using System.Globalization;

namespace MovieAPI.Persistence.Data
{
    public class CsvFileReader
    {
        public static List<Movie> ReadCsvData(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ",",
            };
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                csv.Read();
                csv.ReadHeader();
                List<Movie> movies = new List<Movie>();
                while (csv.Read())
                {
                    try
                    {
                        var record = new Movie
                        {
                            ReleaseDate = DateTime.ParseExact(csv.GetField("Release_Date"), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            Title = csv.GetField("Title"),
                            Overview = csv.GetField("Overview"),
                            Popularity = decimal.Parse(csv.GetField("Popularity")),
                            VoteCount = int.Parse(csv.GetField("Vote_Count")),
                            VoteAverage = decimal.Parse(csv.GetField("Vote_Average")),
                            OriginalLanguage = csv.GetField("Original_Language"),
                            Genre = csv.GetField("Genre"),
                            PosterUrl = csv.GetField("Poster_Url"),
                            CreatedBy = "",
                            LastModified = ""
                        };

                        movies.Add(record);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());

                    }

                }
                return movies;
            }
        }
    }
}
