using System;
using System.Collections.Generic;
using app.ViewModels;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace app.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class GenresController : Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenresController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public IActionResult Index()
        {
            var genres = _genreRepository.GetAll();
            var genresVm = Mapper.Map<IEnumerable<Genre>, IEnumerable<GenreViewModel>>(genres);
            return new OkObjectResult(genresVm);
        }

        [HttpGet("{id}", Name = "GetGenre")]
        public IActionResult Get(int id)
        {
            var genre = _genreRepository.GetSingle(id);
            var genreVm = Mapper.Map<Genre, GenreViewModel>(genre);
            return new OkObjectResult(genreVm);
        }

        [HttpPost]
        public IActionResult Create([FromBody] GenreViewModel genreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newGenre = Mapper.Map<GenreViewModel, Genre>(genreViewModel);
            _genreRepository.Add(newGenre);          
            _genreRepository.SaveChanges();
            var result = CreatedAtRoute("GetGenre", new { controller = "Genres", id = newGenre.Id }, newGenre);
            return result;
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id,[FromBody] GenreViewModel genreViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genre = _genreRepository.GetSingle(id);
            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                genre.Name = genreViewModel.Name;
                _genreRepository.SaveChanges();
            }
            genreViewModel = Mapper.Map<Genre, GenreViewModel>(genre);
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genre = _genreRepository.GetSingle(id);
            if (genre == null)
            {
                return NotFound();
            }
            else
            {
                _genreRepository.Delete(genre);
                _genreRepository.SaveChanges();
                return new OkObjectResult(new { message = String.Format("The genre which id's {0} has been deleted", id)});
            }
        }
    }
}
