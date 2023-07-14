using MVCStartup.Models.DB.Entities;

namespace MVCStartup.Models.DB.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        Task<User []> GetUsers();
    }
}