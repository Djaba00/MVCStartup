using MVCStartup.Models.DB.Entities;

namespace MVCStartup.Models.DB.Repositories
{
    public interface IRequestRepository
    {
        Task AddRequest(Request request);
        Task<Request []> GetRequests();
    }
}