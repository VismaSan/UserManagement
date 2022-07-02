using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories.Entities;

namespace UserManagement.Repositories.DbContext;

public class PostgreSqlContext : Microsoft.EntityFrameworkCore.DbContext
{
    public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
        : base(options)
    {
    }

    public DbSet<UserEntity> users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
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