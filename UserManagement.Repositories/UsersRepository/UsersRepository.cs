using Microsoft.Extensions.Logging;
using UserManagement.Repositories.Entities;

namespace UserManagement.Repositories.UsersRepository;

public class UsersRepository : IUsersRepository
{
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(ILogger<UsersRepository> logger)
    {
        _logger = logger;
    }

    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        return await Task.FromResult(MockedUsers);
    }

    public async Task<UserEntity?> GetUser(int userId)
    {
        UserEntity? result = MockedUsers.FirstOrDefault(user => user.Id == userId);

        return await Task.FromResult(result);
    }

    public Task<UserEntity> CreateUser(UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity> UpdateUser(int userId, UserEntity user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUser(int userId)
    {
        throw new NotImplementedException();
    }

    private static IEnumerable<UserEntity> MockedUsers => new List<UserEntity>
    {
        new()
        {
            Id = 1,
            Username = "User0",
            Email = "Email0"
        },
        new()
        {
            Id = 10,
            Username = "User10",
            Email = "Email10"
        }
    };
}