using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class HomeController : Controller
{
    private readonly IQuizService _quizService;
    private readonly IUserService _userService;

    public HomeController(IQuizService quizService, IUserService userService)
    {
        _quizService = quizService;
        _userService = userService;
    }

    public IActionResult Index()
    {
        var activeQuizzes = _quizService.GetAllQuizzes(activeOnly: true);
        var featuredQuizzes = activeQuizzes.Take(6).ToList();

        ViewBag.TotalQuizzes = activeQuizzes.Count;
        ViewBag.TotalQuestions = activeQuizzes.Sum(q => q.Questions.Count);
        ViewBag.TotalStudents = _userService.GetTotalStudentsCount();
        ViewBag.TotalAttempts = _quizService.GetAllAttempts().Count;
        ViewBag.Categories = _quizService.GetCategories();

        return View(featuredQuizzes);
    }

    public IActionResult About()
    {
        ViewBag.TotalQuizzes = _quizService.GetAllQuizzes().Count;
        ViewBag.TotalQuestions = _quizService.GetAllQuizzes().Sum(q => q.Questions.Count);
        ViewBag.TotalStudents = _userService.GetTotalStudentsCount();

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
