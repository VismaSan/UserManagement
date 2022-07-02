using UserManagement.Repositories.Entities;
using UserManagement.Services.Models;

namespace UserManagement.Services.MapperExtensions;

public static class UserMapper
{
    public static UserEntity ToEntity(this User model)
    {
        return new UserEntity
        {
            Id = model.Id,
            Username = model.Username,
            Email = model.Email
        };
    }

    public static User ToModel(this UserEntity entity)
    {
        return new User
        {
            Id = entity.Id,
            Username = entity.Username,
            Email = entity.Email
        };
    }
}