using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ArtistRepository : EfRepository<Artist>, IArtistRepository
    {
        public ArtistRepository(MusicDbContext musicDbContext) : base(musicDbContext)
        {
            
        }
    }
}