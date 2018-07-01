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
    //[Consumes("application/json", "application/json-patch+json", "multipart/form-data")]
    public class TracksController : Controller
    {
        private readonly ITrackRepository _trackRepository;
        private readonly IHostingEnvironment _hostingEnvironment;
        
        public TracksController(ITrackRepository trackRepository, IHostingEnvironment hostingEnvironment)
        {
            _trackRepository = trackRepository;
            _hostingEnvironment = hostingEnvironment;
        }
        // GET: get all tracks
        [HttpGet]
        public IActionResult Index()
        {
            var tracks = _trackRepository.GetAll();
            var tracksVm = Mapper.Map<IEnumerable<Track>, IEnumerable<TrackViewModel>>(tracks);
            return new OkObjectResult(tracksVm);
        }

        // GET: get single track
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var track = _trackRepository.GetSingle(id);
            if (track == null)
            {
                return NotFound();
            }
            else
            {
                var trackVm = Mapper.Map<Track, TrackViewModel>(track);
                return new OkObjectResult(trackVm);
            }
        }
        // POST: Create track
        [HttpPost]
        public async Task<IActionResult> Create(TrackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var trackVm = Mapper.Map<TrackViewModel, Track>(model);
            if (model.TrackFile?.Length > 0)
            {
                
                var root = _hostingEnvironment.WebRootPath;
                var path = Path.Combine(root,"musics", model.TrackFile.FileName);
                trackVm.Path = Path.Combine("musics", model.TrackFile.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.TrackFile.CopyToAsync(stream);
                }
                _trackRepository.Add(trackVm);
                _trackRepository.SaveChanges();
            }
            return new OkObjectResult(trackVm);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var track = _trackRepository.GetSingle(x => x.Id == id);
            if (track == null)
            {
                return new NotFoundResult();
            }
            else
            {
                _trackRepository.Delete(track);
                _trackRepository.SaveChanges();
                return new OkObjectResult(new { message = string.Format("{0} id was deleted", id)});
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TrackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var track = _trackRepository.GetSingle(id);
            if (track == null)
            {
                return NotFound();
            }
            else
            {
                track.AlbumId = model.AlbumId;
                track.TrackName = model.TrackName;
                track.TrackNumber = model.TrackNumber;
                if (model.TrackFile?.Length > 0)
                {
                    var root = _hostingEnvironment.WebRootPath;
                    var path = Path.Combine(root,"musics", model.TrackFile.FileName);
                    track.Path = Path.Combine("musics", model.TrackFile.FileName);
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await model.TrackFile.CopyToAsync(stream);
                    }
                }
                _trackRepository.SaveChanges();
            }
            model = Mapper.Map<Track, TrackViewModel>(track);
            return new NoContentResult();
        }
    }
}