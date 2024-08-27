using Microsoft.EntityFrameworkCore;
using Persistence.Models;

namespace Persistence.DatabaseContext;

public class TableContext : DbContext
{
    public DbSet<Todo> Todo { get; set; }
    public DbSet<TodoDetail> TodoDetail { get; set; }
    public DbSet<TableSpecification> TableSpecifications { get; set; }
    
    public TableContext(DbContextOptions<TableContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableDetailedErrors();
    }
}