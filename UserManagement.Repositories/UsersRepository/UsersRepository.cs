using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UserManagement.Repositories.DbContext;
using UserManagement.Repositories.Entities;

namespace UserManagement.Repositories.UsersRepository;

public class UsersRepository : IUsersRepository
{
    private readonly PostgreSqlContext _dbContext;
    private readonly ILogger<UsersRepository> _logger;

    public UsersRepository(PostgreSqlContext dbContext, ILogger<UsersRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<IEnumerable<UserEntity>> GetUsers()
    {
        return await _dbContext.users.ToListAsync();
    }

    public async Task<UserEntity?> GetUser(int userId)
    {
        return await _dbContext.users.Where(user => user.id == userId).FirstOrDefaultAsync();
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
            id = 1,
            username = "User0",
            email = "Email0"
        },
        new()
        {
            id = 10,
            username = "User10",
            email = "Email10"
        }
    };
}