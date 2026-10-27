using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class StudentController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;

    public StudentController(IQuizService quizService, IUserService userService)
    {
        _quizService = quizService;
        _userService = userService;
    }

    private bool EnsureStudent(out int userId, out string userName)
    {
        userId = AuthHelper.GetUserId(HttpContext.Session) ?? 0;
        userName = AuthHelper.GetUserName(HttpContext.Session);

        if (!AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            return false;
        }

        // Allow Students or Admins to view
        return true;
    }

    public IActionResult Dashboard()
    {
        if (!EnsureStudent(out int userId, out string userName))
        {
            TempData["ErrorMessage"] = "Please login to access your dashboard.";
            return RedirectToAction("Login", "Account", new { returnUrl = "/Student/Dashboard" });
        }

        var stats = _quizService.GetStudentStats(userId);
        ViewBag.UserName = userName;
        return View(stats);
    }

    public IActionResult History()
    {
        if (!EnsureStudent(out int userId, out string userName))
        {
            TempData["ErrorMessage"] = "Please login to view your quiz history.";
            return RedirectToAction("Login", "Account", new { returnUrl = "/Student/History" });
        }

        var attempts = _quizService.GetUserAttempts(userId);
        ViewBag.UserName = userName;
        return View(attempts);
    }

    [HttpGet]
    public IActionResult Profile()
    {
        if (!EnsureStudent(out int userId, out _))
        {
            TempData["ErrorMessage"] = "Please login to view your profile.";
            return RedirectToAction("Login", "Account", new { returnUrl = "/Student/Profile" });
        }

        var user = _userService.GetById(userId);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var stats = _quizService.GetStudentStats(userId);
        var viewModel = new ProfileViewModel
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role,
            JoinedDate = user.JoinedDate,
            ProfileImage = user.ProfileImage,
            QuizzesTaken = stats.QuizzesTaken,
            AverageScore = stats.AverageScore,
            BestScore = stats.BestScore,
            PassedQuizzes = stats.PassedQuizzes
        };

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult EditProfile(EditProfileViewModel model)
    {
        if (!EnsureStudent(out int userId, out _))
        {
            return RedirectToAction("Login", "Account");
        }

        if (!ModelState.IsValid)
        {
            TempData["ErrorMessage"] = "Please check the form for invalid inputs.";
            return RedirectToAction("Profile");
        }

        var (success, message) = _userService.UpdateProfile(userId, model.FullName, model.Email, model.CurrentPassword, model.NewPassword);
        if (success)
        {
            // Update session name and email
            HttpContext.Session.SetString(AuthHelper.SessionUserName, model.FullName);
            HttpContext.Session.SetString(AuthHelper.SessionUserEmail, model.Email);
            TempData["SuccessMessage"] = message;
        }
        else
        {
            TempData["ErrorMessage"] = message;
        }

        return RedirectToAction("Profile");
    }
}
