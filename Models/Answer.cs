namespace QuizMaster.Models;

public class Answer
{
    public int QuestionId { get; set; }
    public string SelectedAnswer { get; set; } = string.Empty; // "A", "B", "C", "D" or empty
    public string CorrectAnswer { get; set; } = string.Empty;
    public bool IsCorrect { get; set; }
}
