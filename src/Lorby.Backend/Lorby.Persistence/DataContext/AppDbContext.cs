using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Persistence.DataContext;

/// <summary>
/// Represents Database Context for the entire web application
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> dbContext) : DbContext(dbContext)
{
    DbSet<User> Users => Set<User>();

    public DbSet<AccessToken> AccessTokens => Set<AccessToken>();

    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    
    public DbSet<VerificationCode> Verifications => Set<VerificationCode>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

