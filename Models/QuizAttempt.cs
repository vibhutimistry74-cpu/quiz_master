namespace QuizMaster.Models;

public class QuizAttempt
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int QuizId { get; set; }
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public double Percentage { get; set; }
    public int CorrectAnswers { get; set; }
    public int WrongAnswers { get; set; }
    public int Unanswered { get; set; }
    public string Status { get; set; } = "Failed"; // "Passed" or "Failed"
    public DateTime StartedAt { get; set; } = DateTime.UtcNow;
    public DateTime CompletedAt { get; set; } = DateTime.UtcNow;

    // Detailed answers for review
    public List<Answer> UserAnswers { get; set; } = new();

    // Display & reporting helpers
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public string QuizTitle { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Difficulty { get; set; } = string.Empty;
    public int TimeSpentSeconds => Math.Max(0, (int)(CompletedAt - StartedAt).TotalSeconds);
}
