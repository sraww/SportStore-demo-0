using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class SQLiteContext : DbContext
    {
        public SQLiteContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<UserSQLite> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            if (!optionBuilder.IsConfigured)
            {
                // SQLite
                optionBuilder.UseSqlite(@"DataSource=SportStore.db;");
            }
        }
    }
}
