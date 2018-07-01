using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext() {}
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options) { }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Track> Tracks { get; set; }
    }
}