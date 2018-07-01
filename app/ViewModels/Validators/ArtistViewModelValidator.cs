using FluentValidation;

namespace app.ViewModels.Validators
{
    public class ArtistViewModelValidator : AbstractValidator<ArtistViewModel>
    {
        public ArtistViewModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Artist name cannot be empty");
            RuleFor(x => x.ShortBio).NotEmpty().WithMessage("Artist biography cannot be empty");
            
        }
    }
}