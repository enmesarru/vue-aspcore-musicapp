using System;

namespace ApplicationCore.Entities
{
    public class Track : IBaseEntity
    {
        public int Id { get; set; }
        public string TrackName { get; set; }
        public byte TrackNumber { get; set; }
        public string Path { get; set; }
        public int AlbumId { get; set; }
        public Album Album { get; set; }
        
        public DateTime Updated { get; private set; } = DateTime.Now;
    }
}
