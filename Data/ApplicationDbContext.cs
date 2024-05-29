using Microsoft.EntityFrameworkCore;

public class ApplicationContext : DbContext
{
    //public DbSet<Funtranslate> Funtranslates => Set<Funtranslate>();
    public DbSet<FunAPITranslate> FunAPITranslates { get; set; } = null!;
    public ApplicationContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=;database=translatelang;",
            new MySqlServerVersion(new Version(10, 4, 32)));
    }
}