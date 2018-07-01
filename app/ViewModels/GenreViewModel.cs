using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using app.ViewModels.Validators;
using FluentValidation.Attributes;

namespace app.ViewModels
{
    [Validator(typeof(GenreViewModelValidator))]
    public class GenreViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}