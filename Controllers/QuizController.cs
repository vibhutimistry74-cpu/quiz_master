using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class QuizController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IQuizSessionService _sessionService;

    public QuizController(IQuizService quizService, IQuizSessionService sessionService)
    {
        _quizService = quizService;
        _sessionService = sessionService;
    }

    public IActionResult Index(string? search, string? category, string? difficulty, string? sortBy)
    {
        var quizzes = _quizService.GetFilteredQuizzes(search, category, difficulty, sortBy);

        var viewModel = new QuizFilterViewModel
        {
            SearchTerm = search,
            Category = category,
            Difficulty = difficulty,
            SortBy = sortBy,
            Quizzes = quizzes,
            Categories = _quizService.GetCategories(),
            Difficulties = _quizService.GetDifficulties()
        };

        return View(viewModel);
    }

    public IActionResult Details(int id)
    {
        var quiz = _quizService.GetQuizById(id);
        if (quiz == null || !quiz.IsActive)
        {
            TempData["ErrorMessage"] = "Quiz not found or is currently inactive.";
            return RedirectToAction(nameof(Index));
        }

        return View(quiz);
    }

    public IActionResult Instructions(int id)
    {
        var quiz = _quizService.GetQuizById(id);
        if (quiz == null || !quiz.IsActive)
        {
            TempData["ErrorMessage"] = "Quiz not found or is currently inactive.";
            return RedirectToAction(nameof(Index));
        }

        return View(quiz);
    }

    public IActionResult Start(int id)
    {
        if (!AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            TempData["ErrorMessage"] = "Please login before starting a quiz.";
            return RedirectToAction("Login", "Account", new { returnUrl = $"/Quiz/Instructions/{id}" });
        }

        var quiz = _quizService.GetQuizById(id);
        if (quiz == null || !quiz.IsActive || quiz.Questions.Count == 0)
        {
            TempData["ErrorMessage"] = "This quiz is not currently available for examination.";
            return RedirectToAction(nameof(Index));
        }

        // Initialize quiz session
        _sessionService.StartQuizSession(quiz.Id, quiz.DurationMinutes);

        return RedirectToAction(nameof(Play), new { id = quiz.Id, qIndex = 0 });
    }

    public IActionResult Play(int id, int qIndex = 0)
    {
        if (!AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            TempData["ErrorMessage"] = "Please login to take this quiz.";
            return RedirectToAction("Login", "Account", new { returnUrl = $"/Quiz/Play/{id}" });
        }

        var quiz = _quizService.GetQuizById(id);
        if (quiz == null || !quiz.IsActive || quiz.Questions.Count == 0)
        {
            TempData["ErrorMessage"] = "Quiz not found or has no questions.";
            return RedirectToAction(nameof(Index));
        }

        var sessionState = _sessionService.GetCurrentSession(id);
        if (sessionState == null)
        {
            // If session was lost or expired, start a new one
            sessionState = _sessionService.StartQuizSession(quiz.Id, quiz.DurationMinutes);
        }

        var remainingSeconds = sessionState.GetRemainingSeconds();
        if (remainingSeconds <= 0)
        {
            TempData["WarningMessage"] = "Time has expired! Submitting your quiz now.";
            return RedirectToAction(nameof(Submit), new { id = quiz.Id });
        }

        // Clamp question index
        if (qIndex < 0) qIndex = 0;
        if (qIndex >= quiz.Questions.Count) qIndex = quiz.Questions.Count - 1;

        _sessionService.SetCurrentIndex(id, qIndex);

        var viewModel = new QuizPlayViewModel
        {
            Quiz = quiz,
            Questions = quiz.Questions,
            CurrentIndex = qIndex,
            Answers = sessionState.Answers,
            RemainingSeconds = remainingSeconds
        };

        return View(viewModel);
    }

    [HttpPost]
    public IActionResult SaveAnswer([FromBody] SaveAnswerRequest request)
    {
        if (request == null || request.QuizId <= 0 || request.QuestionId <= 0)
        {
            return BadRequest(new { success = false, message = "Invalid request." });
        }

        _sessionService.SaveAnswer(request.QuizId, request.QuestionId, request.SelectedOption ?? string.Empty);
        return Json(new { success = true });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Submit(int id)
    {
        if (!AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            return RedirectToAction("Login", "Account");
        }

        var userId = AuthHelper.GetUserId(HttpContext.Session) ?? 0;
        var userName = AuthHelper.GetUserName(HttpContext.Session);
        var userEmail = AuthHelper.GetUserEmail(HttpContext.Session);

        var quiz = _quizService.GetQuizById(id);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = "Quiz not found.";
            return RedirectToAction(nameof(Index));
        }

        var sessionState = _sessionService.GetCurrentSession(id);
        var userAnswers = sessionState?.Answers ?? new Dictionary<int, string>();
        var startedAt = sessionState?.StartedAt ?? DateTime.UtcNow.AddMinutes(-quiz.DurationMinutes);

        var attempt = _quizService.SubmitAttempt(userId, userName, userEmail, quiz.Id, userAnswers, startedAt);

        // Clear session after successful submission
        _sessionService.ClearSession(id);

        TempData["SuccessMessage"] = "Quiz submitted successfully!";
        return RedirectToAction(nameof(Result), new { attemptId = attempt.Id });
    }

    public IActionResult Result(int attemptId)
    {
        var attempt = _quizService.GetAttemptById(attemptId);
        if (attempt == null)
        {
            TempData["ErrorMessage"] = "Result not found.";
            return RedirectToAction(nameof(Index));
        }

        var quiz = _quizService.GetQuizById(attempt.QuizId);
        var viewModel = new QuizResultViewModel
        {
            Attempt = attempt,
            Quiz = quiz ?? new Quiz { Title = attempt.QuizTitle },
            Questions = quiz?.Questions ?? new List<Question>()
        };

        return View(viewModel);
    }

    public IActionResult Review(int attemptId)
    {
        var attempt = _quizService.GetAttemptById(attemptId);
        if (attempt == null)
        {
            TempData["ErrorMessage"] = "Attempt not found for answer review.";
            return RedirectToAction(nameof(Index));
        }

        var quiz = _quizService.GetQuizById(attempt.QuizId);
        var viewModel = new QuizResultViewModel
        {
            Attempt = attempt,
            Quiz = quiz ?? new Quiz { Title = attempt.QuizTitle },
            Questions = quiz?.Questions ?? new List<Question>()
        };

        return View(viewModel);
    }
}

public class SaveAnswerRequest
{
    public int QuizId { get; set; }
    public int QuestionId { get; set; }
    public string? SelectedOption { get; set; }
}
