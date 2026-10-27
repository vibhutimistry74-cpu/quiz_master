using QuizMaster.Data;
using QuizMaster.Models;

namespace QuizMaster.Services;

public class UserService : IUserService
{
    private readonly List<User> _users;
    private readonly object _lock = new();

    public UserService()
    {
        _users = DemoData.GetInitialUsers();
    }

    public User? Authenticate(string email, string password)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => 
                string.Equals(u.Email, email.Trim(), StringComparison.OrdinalIgnoreCase) && 
                u.Password == password);

            if (user != null && !user.IsActive)
            {
                return null; // Deactivated account
            }

            return user;
        }
    }

    public (bool Success, string Message, User? User) Register(string fullName, string email, string password)
    {
        lock (_lock)
        {
            if (_users.Any(u => string.Equals(u.Email, email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "An account with this email already exists.", null);
            }

            var nextId = _users.Count > 0 ? _users.Max(u => u.Id) + 1 : 1;
            var newUser = new User
            {
                Id = nextId,
                FullName = fullName.Trim(),
                Email = email.Trim().ToLowerInvariant(),
                Password = password,
                Role = "Student",
                ProfileImage = "/images/avatars/student1.png",
                JoinedDate = DateTime.UtcNow,
                IsActive = true
            };

            _users.Add(newUser);
            return (true, "Registration successful. Please login.", newUser);
        }
    }

    public User? GetById(int id)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }
    }

    public User? GetByEmail(string email)
    {
        lock (_lock)
        {
            return _users.FirstOrDefault(u => string.Equals(u.Email, email.Trim(), StringComparison.OrdinalIgnoreCase));
        }
    }

    public List<User> GetAllUsers()
    {
        lock (_lock)
        {
            return _users.OrderByDescending(u => u.JoinedDate).ToList();
        }
    }

    public bool ToggleUserStatus(int id)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null && user.Role != "Admin") // Protect main admin
            {
                user.IsActive = !user.IsActive;
                return true;
            }
            return false;
        }
    }

    public (bool Success, string Message) UpdateProfile(int id, string fullName, string email, string? currentPassword, string? newPassword)
    {
        lock (_lock)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return (false, "User not found.");
            }

            // Check email collision
            if (_users.Any(u => u.Id != id && string.Equals(u.Email, email.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "This email address is already in use by another account.");
            }

            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (string.IsNullOrEmpty(currentPassword) || user.Password != currentPassword)
                {
                    return (false, "Current password is incorrect.");
                }
                user.Password = newPassword;
            }

            user.FullName = fullName.Trim();
            user.Email = email.Trim();

            return (true, "Profile updated successfully.");
        }
    }

    public int GetTotalStudentsCount()
    {
        lock (_lock)
        {
            return _users.Count(u => u.Role == "Student");
        }
    }
}
