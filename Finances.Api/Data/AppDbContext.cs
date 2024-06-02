using Finances.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Finances.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder mb)
    {
        base.OnModelCreating(mb);

        mb.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); 
    }
}
