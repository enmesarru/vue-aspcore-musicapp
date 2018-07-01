using ApplicationCore.Entities;
using AutoMapper;

namespace app.ViewModels.DomainMapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile() 
        {
            CreateMap<Artist, ArtistViewModel>()
                .ForMember(artist => artist.Name, map => map.MapFrom(x => x.Name))
                .ForMember(artist => artist.ShortBio, map => map.MapFrom(x => x.ShortBio))
                .ReverseMap();

             CreateMap<Album, AlbumViewModel>()
                .ForMember(album => album.Genre, map => map.MapFrom(x => x.Genre.Name))
                .ForMember(album => album.Artist, map => map.MapFrom(x => x.Artist.Name))
                .ForMember(album => album.ReleaseDate, map => map.MapFrom(x => x.ReleaseDate))
                .ReverseMap();

            CreateMap<AlbumViewModel, Album>();
            
            CreateMap<Genre, GenreViewModel>()
                .ReverseMap();

            CreateMap<Track, TrackViewModel>()
                .ReverseMap();
            
            CreateMap<TrackViewModel, Track>();
        }
    }
}