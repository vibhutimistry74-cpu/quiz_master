using QuizMaster.Data;
using QuizMaster.Models;

namespace QuizMaster.Services;

public class QuizService : IQuizService
{
    private readonly List<Quiz> _quizzes;
    private readonly List<QuizAttempt> _attempts;
    private readonly object _lock = new();

    public QuizService()
    {
        _quizzes = DemoData.GetInitialQuizzes();
        _attempts = DemoData.GetInitialAttempts();
    }

    public List<Quiz> GetAllQuizzes(bool activeOnly = false)
    {
        lock (_lock)
        {
            var query = _quizzes.AsEnumerable();
            if (activeOnly)
            {
                query = query.Where(q => q.IsActive);
            }
            return query.OrderByDescending(q => q.CreatedDate).ToList();
        }
    }

    public List<Quiz> GetFilteredQuizzes(string? search, string? category, string? difficulty, string? sortBy)
    {
        lock (_lock)
        {
            var query = _quizzes.Where(q => q.IsActive).AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(q => 
                    q.Title.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    q.Description.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(category) && !string.Equals(category, "All", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(q => string.Equals(q.Category, category, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(difficulty) && !string.Equals(difficulty, "All", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(q => string.Equals(q.Difficulty, difficulty, StringComparison.OrdinalIgnoreCase));
            }

            query = sortBy switch
            {
                "Newest" => query.OrderByDescending(q => q.CreatedDate),
                "Difficulty" => query.OrderBy(q => q.Difficulty == "Easy" ? 1 : q.Difficulty == "Medium" ? 2 : 3),
                "Questions" => query.OrderByDescending(q => q.TotalQuestions),
                "Title" => query.OrderBy(q => q.Title),
                _ => query.OrderByDescending(q => q.CreatedDate)
            };

            return query.ToList();
        }
    }

    public Quiz? GetQuizById(int id)
    {
        lock (_lock)
        {
            return _quizzes.FirstOrDefault(q => q.Id == id);
        }
    }

    public Quiz CreateQuiz(QuizFormViewModel model)
    {
        lock (_lock)
        {
            var nextId = _quizzes.Count > 0 ? _quizzes.Max(q => q.Id) + 1 : 1;
            var quiz = new Quiz
            {
                Id = nextId,
                Title = model.Title.Trim(),
                Description = model.Description.Trim(),
                Category = model.Category,
                Difficulty = model.Difficulty,
                DurationMinutes = model.DurationMinutes,
                PassingPercentage = model.PassingPercentage,
                ImageUrl = string.IsNullOrWhiteSpace(model.ImageUrl) ? "/images/quiz-default.svg" : model.ImageUrl.Trim(),
                IsActive = model.IsActive,
                CreatedDate = DateTime.UtcNow,
                Questions = new List<Question>()
            };

            _quizzes.Add(quiz);
            return quiz;
        }
    }

    public bool UpdateQuiz(QuizFormViewModel model)
    {
        lock (_lock)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Id == model.Id);
            if (quiz == null) return false;

            quiz.Title = model.Title.Trim();
            quiz.Description = model.Description.Trim();
            quiz.Category = model.Category;
            quiz.Difficulty = model.Difficulty;
            quiz.DurationMinutes = model.DurationMinutes;
            quiz.PassingPercentage = model.PassingPercentage;
            if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                quiz.ImageUrl = model.ImageUrl.Trim();
            }
            quiz.IsActive = model.IsActive;

            return true;
        }
    }

    public bool DeleteQuiz(int id)
    {
        lock (_lock)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Id == id);
            if (quiz == null) return false;

            _quizzes.Remove(quiz);
            // Optionally remove attempts related to this quiz
            _attempts.RemoveAll(a => a.QuizId == id);
            return true;
        }
    }

    public List<string> GetCategories()
    {
        return new List<string>
        {
            "Programming",
            "Web Development",
            "Database",
            "General Knowledge",
            "Science",
            "Technology"
        };
    }

    public List<string> GetDifficulties()
    {
        return new List<string> { "Easy", "Medium", "Hard" };
    }

    public List<Question> GetQuestionsByQuizId(int quizId)
    {
        lock (_lock)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Id == quizId);
            return quiz != null ? quiz.Questions.ToList() : new List<Question>();
        }
    }

    public Question? GetQuestionById(int id)
    {
        lock (_lock)
        {
            foreach (var quiz in _quizzes)
            {
                var q = quiz.Questions.FirstOrDefault(x => x.Id == id);
                if (q != null) return q;
            }
            return null;
        }
    }

    public Question? AddQuestion(QuestionFormViewModel model)
    {
        lock (_lock)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Id == model.QuizId);
            if (quiz == null) return null;

            // Generate overall unique question id
            var allQuestions = _quizzes.SelectMany(q => q.Questions).ToList();
            var nextId = allQuestions.Count > 0 ? allQuestions.Max(q => q.Id) + 1 : 1;

            var question = new Question
            {
                Id = nextId,
                QuizId = model.QuizId,
                QuestionText = model.QuestionText.Trim(),
                OptionA = model.OptionA.Trim(),
                OptionB = model.OptionB.Trim(),
                OptionC = model.OptionC.Trim(),
                OptionD = model.OptionD.Trim(),
                CorrectAnswer = model.CorrectAnswer.ToUpper().Trim(),
                Explanation = model.Explanation.Trim(),
                Marks = model.Marks
            };

            quiz.Questions.Add(question);
            return question;
        }
    }

    public bool UpdateQuestion(QuestionFormViewModel model)
    {
        lock (_lock)
        {
            var question = GetQuestionById(model.Id);
            if (question == null) return false;

            question.QuestionText = model.QuestionText.Trim();
            question.OptionA = model.OptionA.Trim();
            question.OptionB = model.OptionB.Trim();
            question.OptionC = model.OptionC.Trim();
            question.OptionD = model.OptionD.Trim();
            question.CorrectAnswer = model.CorrectAnswer.ToUpper().Trim();
            question.Explanation = model.Explanation.Trim();
            question.Marks = model.Marks;

            return true;
        }
    }

    public bool DeleteQuestion(int id)
    {
        lock (_lock)
        {
            foreach (var quiz in _quizzes)
            {
                var q = quiz.Questions.FirstOrDefault(x => x.Id == id);
                if (q != null)
                {
                    quiz.Questions.Remove(q);
                    return true;
                }
            }
            return false;
        }
    }

    public QuizAttempt SubmitAttempt(int userId, string studentName, string studentEmail, int quizId, Dictionary<int, string> userAnswers, DateTime startedAt)
    {
        lock (_lock)
        {
            var quiz = _quizzes.FirstOrDefault(q => q.Id == quizId) ?? throw new InvalidOperationException("Quiz not found.");

            var completedAt = DateTime.UtcNow;
            var detailedAnswers = new List<Answer>();
            int correctCount = 0;
            int wrongCount = 0;
            int unansweredCount = 0;
            int score = 0;
            int totalMarks = 0;

            foreach (var question in quiz.Questions)
            {
                totalMarks += question.Marks;
                userAnswers.TryGetValue(question.Id, out var selected);

                var isAnswered = !string.IsNullOrWhiteSpace(selected);
                var isCorrect = isAnswered && string.Equals(selected?.Trim(), question.CorrectAnswer, StringComparison.OrdinalIgnoreCase);

                if (!isAnswered)
                {
                    unansweredCount++;
                }
                else if (isCorrect)
                {
                    correctCount++;
                    score += question.Marks;
                }
                else
                {
                    wrongCount++;
                }

                detailedAnswers.Add(new Answer
                {
                    QuestionId = question.Id,
                    SelectedAnswer = selected ?? string.Empty,
                    CorrectAnswer = question.CorrectAnswer,
                    IsCorrect = isCorrect
                });
            }

            double percentage = totalMarks > 0 ? Math.Round(((double)score / totalMarks) * 100.0, 1) : 0;
            string status = percentage >= quiz.PassingPercentage ? "Passed" : "Failed";

            var nextAttemptId = _attempts.Count > 0 ? _attempts.Max(a => a.Id) + 1 : 1;
            var attempt = new QuizAttempt
            {
                Id = nextAttemptId,
                UserId = userId,
                QuizId = quiz.Id,
                Score = score,
                TotalMarks = totalMarks,
                Percentage = percentage,
                CorrectAnswers = correctCount,
                WrongAnswers = wrongCount,
                Unanswered = unansweredCount,
                Status = status,
                StartedAt = startedAt,
                CompletedAt = completedAt,
                StudentName = studentName,
                StudentEmail = studentEmail,
                QuizTitle = quiz.Title,
                Category = quiz.Category,
                Difficulty = quiz.Difficulty,
                UserAnswers = detailedAnswers
            };

            _attempts.Add(attempt);
            return attempt;
        }
    }

    public QuizAttempt? GetAttemptById(int attemptId)
    {
        lock (_lock)
        {
            return _attempts.FirstOrDefault(a => a.Id == attemptId);
        }
    }

    public List<QuizAttempt> GetUserAttempts(int userId)
    {
        lock (_lock)
        {
            return _attempts
                .Where(a => a.UserId == userId)
                .OrderByDescending(a => a.CompletedAt)
                .ToList();
        }
    }

    public List<QuizAttempt> GetAllAttempts(string? search = null, int? quizId = null, string? status = null)
    {
        lock (_lock)
        {
            var query = _attempts.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var term = search.Trim();
                query = query.Where(a => 
                    a.StudentName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    a.StudentEmail.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                    a.QuizTitle.Contains(term, StringComparison.OrdinalIgnoreCase));
            }

            if (quizId.HasValue && quizId.Value > 0)
            {
                query = query.Where(a => a.QuizId == quizId.Value);
            }

            if (!string.IsNullOrWhiteSpace(status) && !string.Equals(status, "All", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(a => string.Equals(a.Status, status, StringComparison.OrdinalIgnoreCase));
            }

            return query.OrderByDescending(a => a.CompletedAt).ToList();
        }
    }

    public StudentDashboardStats GetStudentStats(int userId)
    {
        lock (_lock)
        {
            var userAttempts = _attempts.Where(a => a.UserId == userId).ToList();
            var activeQuizzes = _quizzes.Where(q => q.IsActive).ToList();

            int quizzesTaken = userAttempts.Count;
            double avgScore = userAttempts.Count > 0 ? Math.Round(userAttempts.Average(a => a.Percentage), 1) : 0;
            double bestScore = userAttempts.Count > 0 ? userAttempts.Max(a => a.Percentage) : 0;
            int passedQuizzes = userAttempts.Count(a => a.Status == "Passed");

            var attemptedQuizIds = userAttempts.Select(a => a.QuizId).ToHashSet();
            var recommended = activeQuizzes
                .Where(q => !attemptedQuizIds.Contains(q.Id))
                .Take(3)
                .ToList();

            if (recommended.Count < 3)
            {
                recommended = activeQuizzes.Take(3).ToList();
            }

            return new StudentDashboardStats
            {
                QuizzesTaken = quizzesTaken,
                AverageScore = avgScore,
                BestScore = bestScore,
                PassedQuizzes = passedQuizzes,
                TotalAvailableQuizzes = activeQuizzes.Count,
                RecentAttempts = userAttempts.OrderByDescending(a => a.CompletedAt).Take(5).ToList(),
                RecommendedQuizzes = recommended
            };
        }
    }

    public AdminDashboardStats GetAdminStats(int totalStudents)
    {
        lock (_lock)
        {
            int totalQuizzes = _quizzes.Count;
            int totalQuestions = _quizzes.Sum(q => q.Questions.Count);
            int totalAttempts = _attempts.Count;
            double avgScore = totalAttempts > 0 ? Math.Round(_attempts.Average(a => a.Percentage), 1) : 0;
            int passedAttempts = _attempts.Count(a => a.Status == "Passed");
            double passRate = totalAttempts > 0 ? Math.Round(((double)passedAttempts / totalAttempts) * 100.0, 1) : 0;

            var mostPop = _attempts.GroupBy(a => a.QuizTitle)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "None";

            var highestScoring = _attempts.GroupBy(a => a.QuizTitle)
                .OrderByDescending(g => g.Average(x => x.Percentage))
                .FirstOrDefault()?.Key ?? "None";

            var categoryCounts = _quizzes.GroupBy(q => q.Category)
                .ToDictionary(g => g.Key, g => g.Count());

            var statusCounts = new Dictionary<string, int>
            {
                { "Passed", passedAttempts },
                { "Failed", totalAttempts - passedAttempts }
            };

            return new AdminDashboardStats
            {
                TotalQuizzes = totalQuizzes,
                TotalQuestions = totalQuestions,
                TotalStudents = totalStudents,
                TotalAttempts = totalAttempts,
                AverageScore = avgScore,
                PassRate = passRate,
                MostPopularQuiz = mostPop,
                HighestScoringQuiz = highestScoring,
                RecentAttempts = _attempts.OrderByDescending(a => a.CompletedAt).Take(5).ToList(),
                CategoryBreakdown = categoryCounts,
                StatusBreakdown = statusCounts
            };
        }
    }

    public LeaderboardViewModel GetLeaderboard()
    {
        lock (_lock)
        {
            var items = _attempts
                .OrderByDescending(a => a.Percentage)
                .ThenBy(a => a.TimeSpentSeconds)
                .ThenByDescending(a => a.CompletedAt)
                .Select((a, index) => new LeaderboardItemViewModel
                {
                    Rank = index + 1,
                    StudentName = a.StudentName,
                    StudentEmail = a.StudentEmail,
                    QuizTitle = a.QuizTitle,
                    Score = a.Score,
                    TotalMarks = a.TotalMarks,
                    Percentage = a.Percentage,
                    Date = a.CompletedAt,
                    Status = a.Status
                })
                .ToList();

            return new LeaderboardViewModel
            {
                Items = items
            };
        }
    }
}
