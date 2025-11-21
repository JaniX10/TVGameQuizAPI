using Microsoft.AspNetCore.Mvc;
using TVGameQuiz.API.Models;

namespace TVGameQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        // ---------------------------------------------------------
        // GET ALL QUIZZES
        // ---------------------------------------------------------
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(QuizDataStore.Quizzes);
        }

        // ---------------------------------------------------------
        // GET QUIZ BY ID
        // ---------------------------------------------------------
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var quiz = QuizDataStore.Quizzes.FirstOrDefault(q => q.Id == id);

            if (quiz == null)
                return NotFound(new { message = "Quiz not found" });

            return Ok(quiz);
        }

        // ---------------------------------------------------------
        // GET QUIZZES BY GENRE
        // ---------------------------------------------------------
        [HttpGet("byGenre/{genreId}")]
        public IActionResult GetByGenre(int genreId)
        {
            var result = QuizDataStore.Quizzes
                .Where(q => q.GenreId == genreId)
                .ToList();

            if (!result.Any())
                return NotFound(new { message = "No quizzes found for this genre" });

            return Ok(result);
        }

        // ---------------------------------------------------------
        // ADD NEW QUIZ (USED ONLY FOR TESTING)
        // ---------------------------------------------------------
        [HttpPost]
        public IActionResult AddQuiz([FromBody] Quiz quiz)
        {
            quiz.Id = QuizDataStore.Quizzes.Count + 1;

            // Assign IDs for questions & options
            foreach (var question in quiz.Questions)
            {
                question.Id = new Random().Next(1000, 9999);

                foreach (var opt in question.Options)
                {
                    opt.Id = new Random().Next(10000, 99999);
                }
            }

            QuizDataStore.Quizzes.Add(quiz);

            return Ok(quiz);
        }
    }
}
