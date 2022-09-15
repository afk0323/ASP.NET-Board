using MagicBoard.Models;
using Microsoft.EntityFrameworkCore;

namespace MagicBoard.DataContext
{
    // 상속 : 부모
    public class MagicBoardDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Note> Notes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=localhost;Database=MagicBoardDbUser;User Id=sa;Password=1234;TrustServerCertificate=True;");

        }
    }
}