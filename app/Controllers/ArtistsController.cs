using System.Collections.Generic;
using System.Linq;
using app.ViewModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ArtistsController : Controller
    {
        private readonly IArtistRepository _artistRepository;
        private readonly IAlbumRepository _albumRepository;
        
        public ArtistsController(IArtistRepository artistRepository, 
            IAlbumRepository albumRepository)
        {
            _artistRepository = artistRepository;
            _albumRepository = albumRepository;
        }

        public IActionResult Index()
        {
            var artist = _artistRepository.GetAll();

            var artistVm = Mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistViewModel>>(artist);
            return new OkObjectResult(artistVm);
        }


        [HttpGet("{id}/albums")]
        public IActionResult GetArtistAlbums(int id)
        {
            var artist = _artistRepository.GetSingle(id);
            if (artist == null)
            {
                return NotFound();
            }
            else
            {
                var albums = _albumRepository.AllIncluding(x => x.Genre, y => y.Artist);
                Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>>(albums);                          
            }

            int totalAlbums = _albumRepository.FindBy(x => x.ArtistId == artist.Id).Count();
            var artistVm =
                Mapper.Map<Artist, ArtistAlbumDetailsViewModel>(artist);
            
            return new OkObjectResult(new { details = artistVm, album_count = totalAlbums });
        }
        
        [HttpGet("{id}", Name = "GetArtistto")]
        public IActionResult Get(int id)
        {
            var artist = _artistRepository.GetSingle(x => x.Id == id);
            if (artist != null)
            {
                var artistVm = Mapper.Map<Artist, ArtistViewModel>(artist);
                return new OkObjectResult(artistVm);
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public IActionResult Create([FromBody] ArtistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // (Artist)
            var newArtist = Mapper.Map<ArtistViewModel, Artist>(model);
            _artistRepository.Add(newArtist);
            _artistRepository.SaveChanges();
            var result = CreatedAtRoute("GetArtistto", new { controller = "Artists", id = newArtist.Id }, newArtist);
            return result;
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var artist = _artistRepository.GetSingle(x => x.Id == id);
            if (artist == null)
            {
               return new NotFoundResult();
            }
            else
            {
                _artistRepository.Delete(artist);
                _artistRepository.SaveChanges();
                return new OkObjectResult(new { message = string.Format("{0} id was deleted", id)});
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ArtistViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var artist = _artistRepository.GetSingle(id);
            if (artist == null)
            {
                return NotFound();
            }
            else
            {
                artist.Name = model.Name;
                artist.ShortBio = model.ShortBio;
                _artistRepository.SaveChanges();
            }

            model = Mapper.Map<Artist, ArtistViewModel>(artist);
            return new NoContentResult();
        }
    }
}