using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class AlbumRepository : EfRepository<Album>, IAlbumRepository
    {
        public AlbumRepository(MusicDbContext musicDbContext) : base(musicDbContext)
        {
            
        }
    }
}