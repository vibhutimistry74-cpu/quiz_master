using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class AdminController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;

    public AdminController(IQuizService quizService, IUserService userService)
    {
        _quizService = quizService;
        _userService = userService;
    }

    private bool CheckAdminAccess()
    {
        if (!AuthHelper.IsAdmin(HttpContext.Session))
        {
            TempData["ErrorMessage"] = "Unauthorized. Administrator access required.";
            return false;
        }
        return true;
    }

    public IActionResult Dashboard()
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Admin/Dashboard" });
        }

        var totalStudents = _userService.GetTotalStudentsCount();
        var stats = _quizService.GetAdminStats(totalStudents);
        return View(stats);
    }

    public IActionResult Quizzes()
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Admin/Quizzes" });
        }

        var quizzes = _quizService.GetAllQuizzes();
        return View(quizzes);
    }

    [HttpGet]
    public IActionResult CreateQuiz()
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        ViewBag.Categories = _quizService.GetCategories();
        ViewBag.Difficulties = _quizService.GetDifficulties();
        return View(new QuizFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateQuiz(QuizFormViewModel model)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _quizService.GetCategories();
            ViewBag.Difficulties = _quizService.GetDifficulties();
            return View(model);
        }

        var quiz = _quizService.CreateQuiz(model);
        TempData["SuccessMessage"] = $"Quiz '{quiz.Title}' created successfully.";
        return RedirectToAction(nameof(Quizzes));
    }

    [HttpGet]
    public IActionResult EditQuiz(int id)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var quiz = _quizService.GetQuizById(id);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = "Quiz not found.";
            return RedirectToAction(nameof(Quizzes));
        }

        var model = new QuizFormViewModel
        {
            Id = quiz.Id,
            Title = quiz.Title,
            Description = quiz.Description,
            Category = quiz.Category,
            Difficulty = quiz.Difficulty,
            DurationMinutes = quiz.DurationMinutes,
            PassingPercentage = quiz.PassingPercentage,
            ImageUrl = quiz.ImageUrl,
            IsActive = quiz.IsActive
        };

        ViewBag.Categories = _quizService.GetCategories();
        ViewBag.Difficulties = _quizService.GetDifficulties();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditQuiz(QuizFormViewModel model)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            ViewBag.Categories = _quizService.GetCategories();
            ViewBag.Difficulties = _quizService.GetDifficulties();
            return View(model);
        }

        var success = _quizService.UpdateQuiz(model);
        if (success)
        {
            TempData["SuccessMessage"] = $"Quiz '{model.Title}' updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to update quiz.";
        }

        return RedirectToAction(nameof(Quizzes));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteQuiz(int id)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var success = _quizService.DeleteQuiz(id);
        if (success)
        {
            TempData["SuccessMessage"] = "Quiz deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Quiz could not be deleted or was not found.";
        }

        return RedirectToAction(nameof(Quizzes));
    }

    public IActionResult Questions(int quizId)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var quiz = _quizService.GetQuizById(quizId);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = "Quiz not found.";
            return RedirectToAction(nameof(Quizzes));
        }

        ViewBag.Quiz = quiz;
        var questions = _quizService.GetQuestionsByQuizId(quizId);
        return View(questions);
    }

    [HttpGet]
    public IActionResult CreateQuestion(int quizId)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var quiz = _quizService.GetQuizById(quizId);
        if (quiz == null)
        {
            TempData["ErrorMessage"] = "Quiz not found.";
            return RedirectToAction(nameof(Quizzes));
        }

        var model = new QuestionFormViewModel
        {
            QuizId = quiz.Id,
            QuizTitle = quiz.Title
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult CreateQuestion(QuestionFormViewModel model)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var created = _quizService.AddQuestion(model);
        if (created != null)
        {
            TempData["SuccessMessage"] = "Question added successfully.";
            return RedirectToAction(nameof(Questions), new { quizId = model.QuizId });
        }

        TempData["ErrorMessage"] = "Failed to add question.";
        return View(model);
    }

    [HttpGet]
    public IActionResult EditQuestion(int id)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var question = _quizService.GetQuestionById(id);
        if (question == null)
        {
            TempData["ErrorMessage"] = "Question not found.";
            return RedirectToAction(nameof(Quizzes));
        }

        var quiz = _quizService.GetQuizById(question.QuizId);
        var model = new QuestionFormViewModel
        {
            Id = question.Id,
            QuizId = question.QuizId,
            QuizTitle = quiz?.Title ?? "Quiz",
            QuestionText = question.QuestionText,
            OptionA = question.OptionA,
            OptionB = question.OptionB,
            OptionC = question.OptionC,
            OptionD = question.OptionD,
            CorrectAnswer = question.CorrectAnswer,
            Explanation = question.Explanation,
            Marks = question.Marks
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditQuestion(QuestionFormViewModel model)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var success = _quizService.UpdateQuestion(model);
        if (success)
        {
            TempData["SuccessMessage"] = "Question updated successfully.";
            return RedirectToAction(nameof(Questions), new { quizId = model.QuizId });
        }

        TempData["ErrorMessage"] = "Failed to update question.";
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteQuestion(int id, int quizId)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var success = _quizService.DeleteQuestion(id);
        if (success)
        {
            TempData["SuccessMessage"] = "Question deleted successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Failed to delete question.";
        }

        return RedirectToAction(nameof(Questions), new { quizId });
    }

    public IActionResult Users()
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Admin/Users" });
        }

        var users = _userService.GetAllUsers();
        return View(users);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult ToggleUserStatus(int id)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account");
        }

        var success = _userService.ToggleUserStatus(id);
        if (success)
        {
            TempData["SuccessMessage"] = "User status updated successfully.";
        }
        else
        {
            TempData["ErrorMessage"] = "Cannot modify primary administrator account.";
        }

        return RedirectToAction(nameof(Users));
    }

    public IActionResult Results(string? search, int? quizId, string? status)
    {
        if (!CheckAdminAccess())
        {
            return RedirectToAction("Login", "Account", new { returnUrl = "/Admin/Results" });
        }

        var attempts = _quizService.GetAllAttempts(search, quizId, status);
        ViewBag.Quizzes = _quizService.GetAllQuizzes();
        ViewBag.SelectedQuizId = quizId;
        ViewBag.SelectedStatus = status;
        ViewBag.SearchTerm = search;

        return View(attempts);
    }
}
