using System.IO;
using ApplicationCore.Interfaces;
using FluentValidation;
using FluentValidation.Results;

namespace app.ViewModels.Validators
{
    public class TrackViewModelValidator : AbstractValidator<TrackViewModel>
    {
        private readonly IAlbumRepository _albumRepository;
        public TrackViewModelValidator(IAlbumRepository albumRepository)
        {
            this._albumRepository = albumRepository;
            RuleFor(x => x.TrackName).NotEmpty().WithMessage("Track name cannot be null");
            RuleFor(x => x.TrackNumber).NotEmpty().WithMessage("Track or tracks number cannot be null");
            RuleFor(x => x.AlbumId).NotEmpty().WithMessage("Album identifier cannot be null");
            When(x => x.TrackFile != null , () =>
            {
                RuleFor(x => x.TrackFile)
                    .Must(x => IsMp3File(x.FileName)).WithMessage("Track file cannot be that except mp3 files");
            });
            
            Custom(rv =>
            {
                var album = _albumRepository.GetSingle(rv.AlbumId);
                if(album == null)
                    return new ValidationFailure("AlbumId","Album Id cannot found");
                return null;
            });
        }

        private bool IsMp3File(string fileName)
        {
            return Path.GetExtension(fileName) == ".mp3";
        }
    }
}