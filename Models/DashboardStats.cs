namespace QuizMaster.Models;

public class StudentDashboardStats
{
    public int QuizzesTaken { get; set; }
    public double AverageScore { get; set; }
    public double BestScore { get; set; }
    public int PassedQuizzes { get; set; }
    public int TotalAvailableQuizzes { get; set; }
    public List<QuizAttempt> RecentAttempts { get; set; } = new();
    public List<Quiz> RecommendedQuizzes { get; set; } = new();
}

public class AdminDashboardStats
{
    public int TotalQuizzes { get; set; }
    public int TotalQuestions { get; set; }
    public int TotalStudents { get; set; }
    public int TotalAttempts { get; set; }
    public double AverageScore { get; set; }
    public double PassRate { get; set; }
    public string MostPopularQuiz { get; set; } = "N/A";
    public string HighestScoringQuiz { get; set; } = "N/A";
    public List<QuizAttempt> RecentAttempts { get; set; } = new();
    public Dictionary<string, int> CategoryBreakdown { get; set; } = new();
    public Dictionary<string, int> StatusBreakdown { get; set; } = new();
}
