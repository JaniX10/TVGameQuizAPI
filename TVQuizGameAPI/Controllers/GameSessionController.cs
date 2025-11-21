using Microsoft.AspNetCore.Mvc;
using TVGameQuiz.API.Models;

namespace TVGameQuiz.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameSessionController : ControllerBase
    {
        private static List<GameSession> sessions = new List<GameSession>();

        [HttpGet]
        public IActionResult GetAll() => Ok(sessions);

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var session = sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return NotFound();
            return Ok(session);
        }

        [HttpPost]
        public IActionResult Start(GameSession session)
        {
            session.Id = sessions.Count + 1;
            session.StartTime = DateTime.Now;
            sessions.Add(session);
            return Ok(session);
        }

        [HttpPost("{id}/submit")]
        public IActionResult Submit(int id, GameSession updated)
        {
            var session = sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return NotFound();

            session.EndTime = DateTime.Now;
            session.Questions = updated.Questions;

            return Ok(session);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var session = sessions.FirstOrDefault(s => s.Id == id);
            if (session == null) return NotFound();

            sessions.Remove(session);
            return Ok("Game session deleted");
        }
    }
}
