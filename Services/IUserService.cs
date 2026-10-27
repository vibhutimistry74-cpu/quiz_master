using QuizMaster.Models;

namespace QuizMaster.Services;

public interface IUserService
{
    User? Authenticate(string email, string password);
    (bool Success, string Message, User? User) Register(string fullName, string email, string password);
    User? GetById(int id);
    User? GetByEmail(string email);
    List<User> GetAllUsers();
    bool ToggleUserStatus(int id);
    (bool Success, string Message) UpdateProfile(int id, string fullName, string email, string? currentPassword, string? newPassword);
    int GetTotalStudentsCount();
}
