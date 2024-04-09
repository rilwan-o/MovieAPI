using FluentValidation;
using MovieAPI.Application.Features.Models;

namespace MovieAPI.Application.Validations
{
    public class MovieSearchValidator : AbstractValidator<MovieSearch>
    {
        public MovieSearchValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title cannot be empty");
            RuleFor(x => x.Page).GreaterThan(0).WithMessage("Page must be greater than 0");
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("PageSize must be greater than 0");
        }
    }  
}
