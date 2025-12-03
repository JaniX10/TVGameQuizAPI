namespace TVGameQuiz.API.Models
{
    public class Result
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int QuizId { get; set; }
        public int Score { get; set; }

        public string Difficulty { get; set; }
        public int TimeTaken { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
