using UserManagement.Services.Models;

namespace UserManagement.Services.UsersService;

public interface IUsersService
{
    Task<IEnumerable<User>> GetUsers();
    Task<User?> GetUser(int id);
    Task<User> CreateUser(User user);
    Task<bool> UpdateUser(int userId, User user);
    Task<bool> DeleteUser(int userId);
}