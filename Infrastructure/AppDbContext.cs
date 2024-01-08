using Microsoft.EntityFrameworkCore;

// Representação de um banco de dados
public class AppDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite("DataSource=MinimalApis.db;Cache=Shared");
}