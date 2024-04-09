using MovieAPI.Domin.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieAPI.Domin.Entities
{
    public class Movie : AuditableEntities
    {
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Title { get; set; }
        public string Overview { get; set; }
        public decimal Popularity { get; set; }
        public int VoteCount { get; set; }
        public decimal VoteAverage { get; set; }
        public string OriginalLanguage { get; set; }
        public string Genre { get; set; }
        public string PosterUrl { get; set; }
    }
}
