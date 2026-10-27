namespace QuizMaster.Models;

public class Quiz
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = "Programming"; // Programming, Web Development, Database, General Knowledge, Science, Technology
    public string Difficulty { get; set; } = "Easy"; // Easy, Medium, Hard
    public int DurationMinutes { get; set; } = 10;
    public int TotalQuestions => Questions.Count;
    public int PassingPercentage { get; set; } = 50;
    public string ImageUrl { get; set; } = "/images/quiz-default.svg";
    public bool IsActive { get; set; } = true;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public List<Question> Questions { get; set; } = new();
}
