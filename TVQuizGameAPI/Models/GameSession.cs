namespace TVGameQuiz.API.Models
{
    public class GameSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int QuizId { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public List<QuestionSession> Questions { get; set; }
    }

    public class QuestionSession
    {
        public string Question { get; set; }
        public List<string> Options { get; set; }
        public string SubmittedAnswer { get; set; }
    }
}
