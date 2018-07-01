using FluentValidation;

namespace app.ViewModels.Validators
{
    public class GenreViewModelValidator : AbstractValidator<GenreViewModel>
    {
        public GenreViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Genre name cannot be empty");
        }
    }
}