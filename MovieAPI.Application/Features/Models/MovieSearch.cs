using MovieAPI.Application.Contracts.Enums;

namespace MovieAPI.Application.Features.Models
{
    public class MovieSearch
    {
        public string Title { get; set; }
        public Genre? Genre { get; set; }
        public SortBy? SortBy { get; set; }
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
