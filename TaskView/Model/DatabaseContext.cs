using Microsoft.EntityFrameworkCore;

namespace TaskView.Model
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Project> projects { get; set; } = null!;
        public DbSet<Employer> employers { get; set; }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
