using Microsoft.AspNetCore.Mvc;
using QuizMaster.Models;
using QuizMaster.Services;

namespace QuizMaster.Controllers;

public class AccountController : Controller
{
    private readonly IUserService _userService;

    public AccountController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        if (AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            var role = AuthHelper.GetUserRole(HttpContext.Session);
            return role == "Admin" ? RedirectToAction("Dashboard", "Admin") : RedirectToAction("Dashboard", "Student");
        }

        ViewBag.ReturnUrl = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Login(LoginViewModel model, string? returnUrl = null)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = _userService.Authenticate(model.Email, model.Password);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "Invalid email or password.");
            return View(model);
        }

        if (!user.IsActive)
        {
            ModelState.AddModelError(string.Empty, "Your account has been deactivated. Please contact an administrator.");
            return View(model);
        }

        // Save into session
        AuthHelper.SetUser(HttpContext.Session, user);
        TempData["SuccessMessage"] = $"Welcome back, {user.FullName}!";

        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }

        return user.Role == "Admin" 
            ? RedirectToAction("Dashboard", "Admin") 
            : RedirectToAction("Dashboard", "Student");
    }

    [HttpGet]
    public IActionResult Register()
    {
        if (AuthHelper.IsLoggedIn(HttpContext.Session))
        {
            return RedirectToAction("Dashboard", "Student");
        }

        return View(new RegisterViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var (success, message, user) = _userService.Register(model.FullName, model.Email, model.Password);
        if (!success)
        {
            ModelState.AddModelError(string.Empty, message);
            return View(model);
        }

        TempData["SuccessMessage"] = "Registration successful. Please login.";
        return RedirectToAction("Login");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Logout()
    {
        AuthHelper.ClearUser(HttpContext.Session);
        TempData["SuccessMessage"] = "You have been successfully logged out.";
        return RedirectToAction("Index", "Home");
    }
}
