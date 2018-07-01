using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class TrackRepository : EfRepository<Track>, ITrackRepository
    {
        public TrackRepository(MusicDbContext musicDbContext) : base(musicDbContext)
        {
            
        }
    }
}