using UserManagement.Repositories.Entities;
using UserManagement.Services.Models;

namespace UserManagement.Services.MapperExtensions;

public static class UserMapper
{
    public static UserEntity ToEntity(this User model)
    {
        return new UserEntity
        {
            id = model.Id,
            username = model.Username,
            email = model.Email
        };
    }

    public static User ToModel(this UserEntity entity)
    {
        return new User
        {
            Id = entity.id,
            Username = entity.username,
            Email = entity.email
        };
    }
}