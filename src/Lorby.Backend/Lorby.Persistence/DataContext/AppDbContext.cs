using Lorby.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lorby.Persistence.DataContext;

/// <summary>
/// Represents Database Context for the entire web application
/// </summary>
public class AppDbContext(DbContextOptions<AppDbContext> dbContext) : DbContext(dbContext)
{
    DbSet<User> Users => Set<User>(); 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}

