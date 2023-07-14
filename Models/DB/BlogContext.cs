using Microsoft.EntityFrameworkCore;
using MVCStartup.Models.DB.Entities;

namespace MVCStartup.Models.DB
{
    /// <summary>
    /// Класс контекста, предоставляющий доступ к сущностям базы данных
    /// </summary>
    public sealed class BlogContext : DbContext
    {
        /// Ссылка на таблицу Users
        public DbSet<User> Users { get; set; }
        
        /// Ссылка на таблицу UserPosts
        public DbSet<UserPost> UserPosts { get; set; }
        public DbSet<Request> Requests { get; set; }
        
        // Логика взаимодействия с таблицами в БД
        public BlogContext(DbContextOptions<BlogContext> options)  : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("Users");
            builder.Entity<UserPost>().ToTable("UsersPosts");
            builder.Entity<Request>().ToTable("Requests");
        }
    }
}