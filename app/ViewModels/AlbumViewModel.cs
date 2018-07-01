using System;
using System.ComponentModel.DataAnnotations;
using app.ViewModels.Validators;
using FluentValidation.Attributes;
using Microsoft.AspNetCore.Http;

namespace app.ViewModels
{
    [Validator(typeof(AlbumViewModelValidator))]
    public class AlbumViewModel // : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string AlbumArtURL { get; set; }
        public string Genre { get; set; }
        public string Artist { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
        public IFormFile Images { get; set; }
        /*public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            
            var validator = new AlbumViewModelValidator();
            var result = validator.Validate(this);
            return result.Errors.Select(x => new ValidationResult(x.ErrorMessage, new[] {x.PropertyName}));
        }*/
    }
}