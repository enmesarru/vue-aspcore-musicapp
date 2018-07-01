using System.Collections.Generic;
using Newtonsoft.Json;

namespace ApplicationCore.Entities
{
    public class Genre : IBaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<Album> Albums { get; set; }
    }
}
