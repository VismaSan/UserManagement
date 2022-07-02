using UserManagement.Repositories.Entities;

namespace UserManagement.Repositories.UsersRepository;

public interface IUsersRepository
{
    Task<IEnumerable<UserEntity>> GetUsers();
    Task<UserEntity?> GetUser(int userId);
    Task<UserEntity> CreateUser(UserEntity user);
    Task<bool> UpdateUser(int userId, UserEntity user);
    Task DeleteUser(UserEntity user);
}