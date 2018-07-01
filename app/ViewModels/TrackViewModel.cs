using System;
using app.ViewModels.Validators;
using FluentValidation.Attributes;
using Microsoft.AspNetCore.Http;

namespace app.ViewModels
{
    [Validator(typeof(TrackViewModelValidator))]
    public class TrackViewModel
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public byte TrackNumber { get; set; }
        public string Path { get; set; }
        public int AlbumId { get; set; }
        public IFormFile TrackFile { get; set; }
    }
}