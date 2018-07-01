using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace ApplicationCore.Entities
{
    public class Album : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public DateTime ReleaseDate { get; set; }
        
        public string AlbumArtURL { get; set; }
        
        [JsonIgnore]
        public Artist Artist { get; set; }
        public int ArtistId { get; set; }

        [JsonIgnore]
        public Genre Genre { get; set; }
        public int GenreId { get; set; }

        [JsonIgnore]
        public ICollection<Track> Tracks { get; set; }            
    }
}
