using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using app.ViewModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AlbumsController : Controller
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AlbumsController(IAlbumRepository albumRepository, IHostingEnvironment hostingEnvironment)
        {
            _albumRepository = albumRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var albums = _albumRepository.AllIncluding(x => x.Artist, y => y.Genre);
            var albumsVm = Mapper.Map<IEnumerable<Album>, IEnumerable<AlbumViewModel>> (albums);
            return new OkObjectResult(albumsVm);
        }

        [HttpGet("{id}", Name = "GetAlbum")]
        public IActionResult Get(int id)
        {
            var album = _albumRepository.GetSingle(x => x.Id == id);
            if (album != null)
            {
                var albumVm = Mapper.Map<Album, AlbumViewModel>(album);
                return new OkObjectResult(albumVm);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var album = _albumRepository.GetSingle(x => x.Id == id);
            if (album == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _albumRepository.Delete(album);
                _albumRepository.SaveChanges();
                return new OkObjectResult(new { message = string.Format("{0} id was deleted", id)});
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(AlbumViewModel albumViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newAlbum = Mapper.Map<AlbumViewModel, Album>(albumViewModel);
            if (albumViewModel.Images?.Length > 0)
            {
                var root = _hostingEnvironment.WebRootPath;
                var path = Path.Combine(root,"img", albumViewModel.Images.FileName);
                newAlbum.AlbumArtURL = Path.Combine("img", albumViewModel.Images.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await albumViewModel.Images.CopyToAsync(stream);
                }             
                _albumRepository.Add(newAlbum);
                _albumRepository.SaveChanges();
            }
            return new OkObjectResult(newAlbum);
        }
        
        [HttpPatch("{id}")]
        public async Task<IActionResult> Put(int id, AlbumViewModel albumViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var album = _albumRepository.GetSingle(x => x.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            else
            {
                album.ArtistId = albumViewModel.ArtistId;
                album.GenreId = albumViewModel.GenreId;
                album.Name = albumViewModel.Name;
                album.ReleaseDate = albumViewModel.ReleaseDate;
                if (albumViewModel.Images?.Length > 0) // NULL PROPAGATION 
                {
                    var root = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine(root,"img", albumViewModel.Images.FileName);
                    album.AlbumArtURL = Path.Combine("img", albumViewModel.Images.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await albumViewModel.Images.CopyToAsync(stream);
                    }
                }
                _albumRepository.SaveChanges();
            }
            albumViewModel = Mapper.Map<Album, AlbumViewModel>(album);
            return new OkObjectResult(albumViewModel);
        }
    }
}