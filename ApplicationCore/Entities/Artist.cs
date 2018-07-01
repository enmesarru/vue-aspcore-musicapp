using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApplicationCore.Entities
{
    public class Artist : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortBio { get; set; }
        [JsonIgnore]
        public ICollection<Album> Albums { get; set; }
    }
}
