namespace TVGameQuiz.API.Models
{
    public class RawQuiz
    {
        public string Title { get; set; }
        public int GenreId { get; set; }

        // IMPORTANT: prevent NULL
        public List<RawQuestion> Questions { get; set; } = new();
    }

    public class RawQuestion
    {
        public string Text { get; set; }

        // IMPORTANT: prevent NULL
        public List<string> Options { get; set; } = new();

        public string CorrectAnswer { get; set; }
    }
}
