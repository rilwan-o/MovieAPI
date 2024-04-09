using FluentValidation.Results;

namespace MovieAPI.Application.Exceptions
{
    public class MovieSearchException : Exception
    {
        public List<ValidationFailure> Failures { get; }

        public MovieSearchException(List<ValidationFailure> failures)
            : base($"An error occurred while searching for movies : '{string.Join(", ", failures)}'")
        {
                Failures = failures;
        }
        
    }
}
