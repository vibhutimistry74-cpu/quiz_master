using Microsoft.AspNetCore.Mvc;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class LeaderboardController : Controller
{
    private readonly IQuizService _quizService;

    public LeaderboardController(IQuizService quizService)
    {
        _quizService = quizService;
    }

    public IActionResult Index()
    {
        var leaderboard = _quizService.GetLeaderboard();
        return View(leaderboard);
    }
}
