using System.ComponentModel.DataAnnotations;

namespace QuizMaster.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
}

public class RegisterViewModel
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Full Name must be between 2 and 100 characters.")]
    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    [Display(Name = "Email Address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirm Password is required.")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Password and confirmation password do not match.")]
    [Display(Name = "Confirm Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class ProfileViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public DateTime JoinedDate { get; set; }
    public string ProfileImage { get; set; } = string.Empty;
    public int QuizzesTaken { get; set; }
    public double AverageScore { get; set; }
    public double BestScore { get; set; }
    public int PassedQuizzes { get; set; }
}

public class EditProfileViewModel
{
    [Required(ErrorMessage = "Full Name is required.")]
    [StringLength(100, MinimumLength = 2)]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    public string? CurrentPassword { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "New password must be at least 6 characters.")]
    [DataType(DataType.Password)]
    public string? NewPassword { get; set; }

    [DataType(DataType.Password)]
    [Compare("NewPassword", ErrorMessage = "New password and confirmation do not match.")]
    public string? ConfirmNewPassword { get; set; }
}

public class QuizFilterViewModel
{
    public string? SearchTerm { get; set; }
    public string? Category { get; set; }
    public string? Difficulty { get; set; }
    public string? SortBy { get; set; } // "Newest", "Difficulty", "Questions", "Title"
    public List<Quiz> Quizzes { get; set; } = new();
    public List<string> Categories { get; set; } = new();
    public List<string> Difficulties { get; set; } = new();
}

public class QuizPlayViewModel
{
    public Quiz Quiz { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
    public int CurrentIndex { get; set; } // 0-indexed
    public Question CurrentQuestion => Questions.Count > CurrentIndex ? Questions[CurrentIndex] : new Question();
    public Dictionary<int, string> Answers { get; set; } = new(); // QuestionId -> Selected Option ("A", "B", "C", "D")
    public int TotalQuestions => Questions.Count;
    public int RemainingSeconds { get; set; }
    public int DurationMinutes => Quiz.DurationMinutes;
    public int AnsweredCount => Answers.Values.Count(a => !string.IsNullOrEmpty(a));
    public int UnansweredCount => TotalQuestions - AnsweredCount;
}

public class QuizResultViewModel
{
    public QuizAttempt Attempt { get; set; } = new();
    public Quiz Quiz { get; set; } = new();
    public List<Question> Questions { get; set; } = new();
}

public class LeaderboardItemViewModel
{
    public int Rank { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public string StudentEmail { get; set; } = string.Empty;
    public string QuizTitle { get; set; } = string.Empty;
    public int Score { get; set; }
    public int TotalMarks { get; set; }
    public double Percentage { get; set; }
    public DateTime Date { get; set; }
    public string Status { get; set; } = "Passed";
}

public class LeaderboardViewModel
{
    public List<LeaderboardItemViewModel> Items { get; set; } = new();
    public List<LeaderboardItemViewModel> TopThree => Items.Take(3).ToList();
    public int TotalParticipants => Items.Select(i => i.StudentEmail).Distinct().Count();
    public int TotalAttempts => Items.Count;
}

public class QuizFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(150, MinimumLength = 3)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Category is required.")]
    public string Category { get; set; } = "Programming";

    [Required(ErrorMessage = "Difficulty is required.")]
    public string Difficulty { get; set; } = "Easy";

    [Required(ErrorMessage = "Duration in minutes is required.")]
    [Range(1, 180, ErrorMessage = "Duration must be between 1 and 180 minutes.")]
    [Display(Name = "Duration (Minutes)")]
    public int DurationMinutes { get; set; } = 10;

    [Required(ErrorMessage = "Passing percentage is required.")]
    [Range(10, 100, ErrorMessage = "Passing percentage must be between 10% and 100%.")]
    [Display(Name = "Passing Percentage")]
    public int PassingPercentage { get; set; } = 50;

    [Display(Name = "Image URL")]
    public string ImageUrl { get; set; } = "/images/quiz-default.svg";

    [Display(Name = "Is Active")]
    public bool IsActive { get; set; } = true;
}

public class QuestionFormViewModel
{
    public int Id { get; set; }

    [Required]
    public int QuizId { get; set; }
    public string QuizTitle { get; set; } = string.Empty;

    [Required(ErrorMessage = "Question text is required.")]
    [StringLength(1000)]
    [Display(Name = "Question Text")]
    public string QuestionText { get; set; } = string.Empty;

    [Required(ErrorMessage = "Option A is required.")]
    [Display(Name = "Option A")]
    public string OptionA { get; set; } = string.Empty;

    [Required(ErrorMessage = "Option B is required.")]
    [Display(Name = "Option B")]
    public string OptionB { get; set; } = string.Empty;

    [Required(ErrorMessage = "Option C is required.")]
    [Display(Name = "Option C")]
    public string OptionC { get; set; } = string.Empty;

    [Required(ErrorMessage = "Option D is required.")]
    [Display(Name = "Option D")]
    public string OptionD { get; set; } = string.Empty;

    [Required(ErrorMessage = "Correct Answer is required.")]
    [RegularExpression("^[ABCD]$", ErrorMessage = "Correct answer must be A, B, C, or D.")]
    [Display(Name = "Correct Answer")]
    public string CorrectAnswer { get; set; } = "A";

    [Required(ErrorMessage = "Explanation is required.")]
    [StringLength(1000)]
    public string Explanation { get; set; } = string.Empty;

    [Required]
    [Range(1, 10, ErrorMessage = "Marks must be between 1 and 10.")]
    public int Marks { get; set; } = 1;
}
