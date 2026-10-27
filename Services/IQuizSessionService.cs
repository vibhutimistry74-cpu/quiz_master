namespace QuizMaster.Services;

public class QuizSessionState
{
    public int QuizId { get; set; }
    public DateTime StartedAt { get; set; }
    public int DurationMinutes { get; set; }
    public int CurrentIndex { get; set; }
    public Dictionary<int, string> Answers { get; set; } = new();

    public int GetRemainingSeconds()
    {
        var elapsed = (DateTime.UtcNow - StartedAt).TotalSeconds;
        var total = DurationMinutes * 60;
        var remaining = (int)(total - elapsed);
        return Math.Max(0, remaining);
    }
}

public interface IQuizSessionService
{
    QuizSessionState StartQuizSession(int quizId, int durationMinutes);
    QuizSessionState? GetCurrentSession(int quizId);
    void SaveAnswer(int quizId, int questionId, string selectedOption);
    void SetCurrentIndex(int quizId, int index);
    void ClearSession(int quizId);
}
