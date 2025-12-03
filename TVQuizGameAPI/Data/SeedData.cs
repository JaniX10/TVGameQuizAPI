using System.Text.Json;
using TVGameQuiz.API.Models;

namespace TVGameQuiz.API.Data
{
    public static class SeedData
    {
        public static void Seed()
        {
            try
            {
                // Correct file path (works in debug + publish)
                var filePath = Path.Combine(
                    AppContext.BaseDirectory,
                    "Data",
                    "quizzes.json"
                );

                Console.WriteLine("================================");
                Console.WriteLine("QUIZ SEEDING STARTED");
                Console.WriteLine("FILE PATH:");
                Console.WriteLine(filePath);
                Console.WriteLine("================================");

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("❌ quizzes.json NOT FOUND!");
                    return;
                }

                Console.WriteLine("✅ quizzes.json FOUND!");
                var json = File.ReadAllText(filePath);

                // 🔥 FIXED: Case-insensitive JSON → C# mapping
                var rawQuizzes = JsonSerializer.Deserialize<List<RawQuiz>>(json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (rawQuizzes == null)
                {
                    Console.WriteLine("❌ rawQuizzes IS NULL!");
                    return;
                }

                Console.WriteLine($"✅ Loaded {rawQuizzes.Count} quizzes from JSON.");

                int quizId = 1;
                int questionId = 1;
                int optionId = 1;

                QuizDataStore.Quizzes.Clear();

                foreach (var rq in rawQuizzes)
                {
                    if (rq.Questions == null)
                    {
                        Console.WriteLine("⚠ rq.Questions was NULL — SKIPPING QUIZ!");
                        continue;
                    }

                    var quiz = new Quiz
                    {
                        Id = quizId++,
                        Title = rq.Title,
                        GenreId = rq.GenreId,
                        Questions = new List<Question>()
                    };

                    foreach (var q in rq.Questions)
                    {
                        var question = new Question
                        {
                            Id = questionId++,
                            Text = q.Text,
                            QuizId = quiz.Id,
                            Options = new List<Option>()
                        };

                        foreach (var optText in q.Options)
                        {
                            question.Options.Add(new Option
                            {
                                Id = optionId++,
                                Text = optText,
                                IsCorrect = (optText == q.CorrectAnswer),
                                QuestionId = question.Id
                            });
                        }

                        quiz.Questions.Add(question);
                    }

                    QuizDataStore.Quizzes.Add(quiz);
                }

                Console.WriteLine("================================");
                Console.WriteLine("🎉 QUIZ SEEDING COMPLETED!");
                Console.WriteLine($"TOTAL QUIZZES LOADED: {QuizDataStore.Quizzes.Count}");
                Console.WriteLine("================================");
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ SEEDING ERROR:");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
