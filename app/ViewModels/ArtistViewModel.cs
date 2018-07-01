using app.ViewModels.Validators;
using FluentValidation.Attributes;

namespace app.ViewModels
{
    [Validator(typeof(ArtistViewModelValidator))]
    public class ArtistViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortBio { get; set; }
    }
}