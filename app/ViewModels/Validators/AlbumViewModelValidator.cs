using System.IO;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace app.ViewModels.Validators
{
    public class AlbumViewModelValidator : AbstractValidator<AlbumViewModel>
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IArtistRepository _artistRepository;
        
        public AlbumViewModelValidator(IGenreRepository genreRepository, 
            IArtistRepository artistRepository)
        {
            this._genreRepository = genreRepository;
            this._artistRepository = artistRepository;

            RuleFor(x => x.Name).NotEmpty().WithMessage("Album name cannot be empty");
            RuleFor(x => x.GenreId).NotEmpty().WithMessage("Genre id cannot be empty");
            When(x => x.Images != null, () =>
            {
                RuleFor(x => x.Images).Must(x => IsImage(x.FileName))
                    .WithMessage("The image cannot be accepted except jpg and png");
            });
            // Custom functions will be renew
            Custom(genreCustom =>
            {
                var genre = _genreRepository.GetSingle(genreCustom.GenreId);
                if (genre == null)
                {
                    return new ValidationFailure("GenreId","Already not exist which you sent genre id");
                }
                return null;
            });
            
            Custom(albumCustom =>
            {
                var album = _artistRepository.GetSingle(albumCustom.ArtistId);
                if (album == null)
                {
                    return new ValidationFailure("ArtistId","Already not exist whic you sent artist id");
                }

                return null;
            });
            
        }

        private bool IsImage(string path)
        {
            var result = false;
            switch (Path.GetExtension(path).ToLower())
            {
                case ".jpg": 
                    result = true;
                    break;
                case ".png": 
                    result = true;
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }
    }
}