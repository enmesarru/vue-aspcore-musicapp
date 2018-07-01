using System.Collections.Generic;

namespace app.ViewModels
{
    public class ArtistAlbumDetailsViewModel
    {
        public int Id { get; set; }        
        public string Name { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
    }
}