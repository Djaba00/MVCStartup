using Microsoft.EntityFrameworkCore;
using MVCStartup.Models.DB.Entities;

namespace MVCStartup.Models.DB.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;
        public RequestRepository(BlogContext contextSet)
        {
            _context = contextSet;
        }

        public async Task AddRequest(Request request)
        {
            request.Id = Guid.NewGuid();
            request.Date = DateTime.Now;

            var entry = _context.Entry(request);

            if(entry.State == EntityState.Detached)
            {
                await _context.Requests.AddAsync(request);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<Request []> GetRequests()
        {
            return await _context.Requests.ToArrayAsync();
        }
    }
}