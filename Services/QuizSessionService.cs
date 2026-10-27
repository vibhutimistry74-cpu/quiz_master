using System.Text.Json;

namespace QuizMaster.Services;

public class QuizSessionService : IQuizSessionService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string SessionKeyPrefix = "QuizSession_";

    public QuizSessionService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ISession? Session => _httpContextAccessor.HttpContext?.Session;

    public QuizSessionState StartQuizSession(int quizId, int durationMinutes)
    {
        var session = Session;
        var key = $"{SessionKeyPrefix}{quizId}";

        // If an active session already exists and hasn't expired, resume it
        if (session != null)
        {
            var existingJson = session.GetString(key);
            if (!string.IsNullOrEmpty(existingJson))
            {
                try
                {
                    var existing = JsonSerializer.Deserialize<QuizSessionState>(existingJson);
                    if (existing != null && existing.GetRemainingSeconds() > 0)
                    {
                        return existing;
                    }
                }
                catch
                {
                    // If corrupted, initialize fresh
                }
            }
        }

        var newState = new QuizSessionState
        {
            QuizId = quizId,
            StartedAt = DateTime.UtcNow,
            DurationMinutes = durationMinutes,
            CurrentIndex = 0,
            Answers = new Dictionary<int, string>()
        };

        SaveState(newState);
        return newState;
    }

    public QuizSessionState? GetCurrentSession(int quizId)
    {
        var session = Session;
        if (session == null) return null;

        var key = $"{SessionKeyPrefix}{quizId}";
        var json = session.GetString(key);
        if (string.IsNullOrEmpty(json)) return null;

        try
        {
            return JsonSerializer.Deserialize<QuizSessionState>(json);
        }
        catch
        {
            return null;
        }
    }

    public void SaveAnswer(int quizId, int questionId, string selectedOption)
    {
        var state = GetCurrentSession(quizId);
        if (state == null) return;

        state.Answers[questionId] = selectedOption.ToUpper().Trim();
        SaveState(state);
    }

    public void SetCurrentIndex(int quizId, int index)
    {
        var state = GetCurrentSession(quizId);
        if (state == null) return;

        state.CurrentIndex = Math.Max(0, index);
        SaveState(state);
    }

    public void ClearSession(int quizId)
    {
        var session = Session;
        if (session == null) return;

        var key = $"{SessionKeyPrefix}{quizId}";
        session.Remove(key);
    }

    private void SaveState(QuizSessionState state)
    {
        var session = Session;
        if (session == null) return;

        var key = $"{SessionKeyPrefix}{state.QuizId}";
        var json = JsonSerializer.Serialize(state);
        session.SetString(key, json);
    }
}
