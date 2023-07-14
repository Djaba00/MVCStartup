using MVCStartup.Models.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace MVCStartup.Models.DB.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BlogContext _context;

        public UserRepository(BlogContext contextSet)
        {
            _context = contextSet;
        }
        public async Task AddUser(User user)
        {
            user.JoinDate = DateTime.Now;
            user.Id = Guid.NewGuid();
            
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);
            
            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task<User []> GetUsers()
        {
            // возвращает массив всех активных пользователей
            return await _context.Users.ToArrayAsync();
        }

        
    }
}