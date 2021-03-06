using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<UserEntity?> GetUser(int userId)
    {
        return await _dbContext.Users.FindAsync(userId);
    }

    public async Task<UserEntity> CreateUser(UserEntity user)
    {
        EntityEntry<UserEntity> createdUser = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return createdUser.Entity;
    }

    public async Task<IEnumerable<UserEntity>> CreateUsers(IEnumerable<UserEntity> users)
    {
        List<UserEntity> result = new();
        foreach (UserEntity userEntity in users)
        {
            EntityEntry<UserEntity> createdEntity = await _dbContext.Users.AddAsync(userEntity);
            result.Add(createdEntity.Entity);
        }

        await _dbContext.SaveChangesAsync();

        return result;
    }

    public async Task<bool> UpdateUser(int userId, UserEntity user)
    {
        UserEntity? currentEntity = await _dbContext.Users.FirstOrDefaultAsync(entity => entity.Id == userId);

        if (currentEntity == null)
        {
            return false;
        }

        _dbContext.Entry(currentEntity).CurrentValues.SetValues(user);
        await _dbContext.SaveChangesAsync();

        return true;
    }

    public async Task DeleteUser(UserEntity user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}