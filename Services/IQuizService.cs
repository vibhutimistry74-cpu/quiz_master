using QuizMaster.Models;

namespace QuizMaster.Services;

public interface IQuizService
{
    // Quizzes
    List<Quiz> GetAllQuizzes(bool activeOnly = false);
    List<Quiz> GetFilteredQuizzes(string? search, string? category, string? difficulty, string? sortBy);
    Quiz? GetQuizById(int id);
    Quiz CreateQuiz(QuizFormViewModel model);
    bool UpdateQuiz(QuizFormViewModel model);
    bool DeleteQuiz(int id);
    List<string> GetCategories();
    List<string> GetDifficulties();

    // Questions
    List<Question> GetQuestionsByQuizId(int quizId);
    Question? GetQuestionById(int id);
    Question? AddQuestion(QuestionFormViewModel model);
    bool UpdateQuestion(QuestionFormViewModel model);
    bool DeleteQuestion(int id);

    // Attempts & Scoring
    QuizAttempt SubmitAttempt(int userId, string studentName, string studentEmail, int quizId, Dictionary<int, string> userAnswers, DateTime startedAt);
    QuizAttempt? GetAttemptById(int attemptId);
    List<QuizAttempt> GetUserAttempts(int userId);
    List<QuizAttempt> GetAllAttempts(string? search = null, int? quizId = null, string? status = null);

    // Analytics & Stats
    StudentDashboardStats GetStudentStats(int userId);
    AdminDashboardStats GetAdminStats(int totalStudents);
    LeaderboardViewModel GetLeaderboard();
}
