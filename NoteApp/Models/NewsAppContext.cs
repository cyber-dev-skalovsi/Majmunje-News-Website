using Microsoft.EntityFrameworkCore;

namespace NoteApp.Models
{
    public class NewsAppContext : DbContext
    {
        public NewsAppContext(DbContextOptions<NewsAppContext> options)
            : base(options) { }
        public DbSet<News> News { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
