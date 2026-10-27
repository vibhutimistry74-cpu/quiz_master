using QuizMaster.Models;

namespace QuizMaster.Services;

public static class AuthHelper
{
    public const string SessionUserId = "UserId";
    public const string SessionUserName = "UserName";
    public const string SessionUserEmail = "UserEmail";
    public const string SessionUserRole = "UserRole";
    public const string SessionIsLoggedIn = "IsLoggedIn";

    public static void SetUser(ISession session, User user)
    {
        session.SetInt32(SessionUserId, user.Id);
        session.SetString(SessionUserName, user.FullName);
        session.SetString(SessionUserEmail, user.Email);
        session.SetString(SessionUserRole, user.Role);
        session.SetString(SessionIsLoggedIn, "true");
    }

    public static void ClearUser(ISession session)
    {
        session.Remove(SessionUserId);
        session.Remove(SessionUserName);
        session.Remove(SessionUserEmail);
        session.Remove(SessionUserRole);
        session.Remove(SessionIsLoggedIn);
    }

    public static bool IsLoggedIn(ISession session)
    {
        return session.GetString(SessionIsLoggedIn) == "true";
    }

    public static int? GetUserId(ISession session)
    {
        return session.GetInt32(SessionUserId);
    }

    public static string GetUserName(ISession session)
    {
        return session.GetString(SessionUserName) ?? "Guest";
    }

    public static string GetUserEmail(ISession session)
    {
        return session.GetString(SessionUserEmail) ?? string.Empty;
    }

    public static string GetUserRole(ISession session)
    {
        return session.GetString(SessionUserRole) ?? string.Empty;
    }

    public static bool IsAdmin(ISession session)
    {
        return IsLoggedIn(session) && string.Equals(GetUserRole(session), "Admin", StringComparison.OrdinalIgnoreCase);
    }

    public static bool IsStudent(ISession session)
    {
        return IsLoggedIn(session) && string.Equals(GetUserRole(session), "Student", StringComparison.OrdinalIgnoreCase);
    }
}
