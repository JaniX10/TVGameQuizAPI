using Microsoft.AspNetCore.Mvc;
using TVGameQuiz.API.Models;

namespace TVGameQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenreController : ControllerBase
    {
        private static List<Genre> genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Science" },
            new Genre { Id = 2, Name = "History" },
            new Genre { Id = 3, Name = "Geography" }
        };

        [HttpGet]
        public IActionResult GetAll() => Ok(genres);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var genre = genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            genre.Id = genres.Count + 1;
            genres.Add(genre);
            return Ok(genre);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Genre updatedGenre)
        {
            var genre = genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();

            genre.Name = updatedGenre.Name;
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var genre = genres.FirstOrDefault(g => g.Id == id);
            if (genre == null) return NotFound();

            genres.Remove(genre);
            return Ok("Genre deleted");
        }
    }
}
