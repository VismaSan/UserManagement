using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories.Entities;
using UserManagement.Repositories.Entities.Maps;

namespace UserManagement.Repositories.DbContext;

public class PostgreSqlContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        new UserEntityMap(builder.Entity<UserEntity>());
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ChangeTracker.DetectChanges();
        return base.SaveChangesAsync(cancellationToken);
    }
}