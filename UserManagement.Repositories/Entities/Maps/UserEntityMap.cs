using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UserManagement.Repositories.Entities.Maps;

public class UserEntityMap
{
    public UserEntityMap(EntityTypeBuilder<UserEntity> entityBuilder)
    {
        entityBuilder.HasKey(entity => entity.Id);
        entityBuilder.ToTable("users");

        entityBuilder.Property(entity => entity.Id).HasColumnName("id");
        entityBuilder.Property(entity => entity.Username).HasColumnName("username");
        entityBuilder.Property(entity => entity.Email).HasColumnName("email");
    }
}