using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using UserManagement.Repositories.Entities;
using UserManagement.Repositories.UsersRepository;
using UserManagement.Services.MapperExtensions;
using UserManagement.Services.Models;

namespace UserManagement.Services.UsersService;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly ILogger<UsersService> _logger;

    public UsersService(IUsersRepository usersRepository, ILogger<UsersService> logger)
    {
        _usersRepository = usersRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> GetUsers()
    {
        IEnumerable<UserEntity> userEntities = await _usersRepository.GetUsers();
        return userEntities.Select(entity => entity.ToModel());
    }

    public async Task<User?> GetUser(int userId)
    {
        UserEntity? userEntity = await _usersRepository.GetUser(userId);
        return userEntity?.ToModel();
    }

    public async Task<User> CreateUser(User user)
    {
        UserEntity createdEntity = await _usersRepository.CreateUser(user.ToEntity());
        return createdEntity.ToModel();
    }

    public async Task<IEnumerable<User>> UploadUsers(string filePath)
    {
        CsvConfiguration csvConfig = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        using StreamReader reader = new(File.OpenRead(filePath));
        using CsvReader csvReader = new(reader, csvConfig);

        IEnumerable<User>? usersToCreate = csvReader.GetRecords<User>();

        if (usersToCreate == null)
        {
            return Enumerable.Empty<User>();
        }

        IEnumerable<UserEntity> entitiesToCreate = usersToCreate.Select(user => user.ToEntity());
        IEnumerable<UserEntity> createdUsers = await _usersRepository.CreateUsers(entitiesToCreate);

        return createdUsers.Select(x => x.ToModel());
    }

    public async Task<bool> UpdateUser(int userId, User user)
    {
        user.Id = userId;
        return await _usersRepository.UpdateUser(userId, user.ToEntity());
    }

    public async Task<bool> DeleteUser(int userId)
    {
        UserEntity? userToRemove = await _usersRepository.GetUser(userId);

        if (userToRemove == null)
        {
            return false;
        }

        await _usersRepository.DeleteUser(userToRemove);

        return true;
    }
}