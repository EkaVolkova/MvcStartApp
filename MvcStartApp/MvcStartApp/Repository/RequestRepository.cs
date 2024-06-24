using Microsoft.EntityFrameworkCore;
using MvcStartApp.Models.Db;
using System.Threading.Tasks;

namespace MvcStartApp.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BlogContext _context;
        public RequestRepository(BlogContext context)
        {
            _context = context;
        }
        public async Task AddRequest(Request request)
        {

            // Добавление пользователя
            var entry = _context.Entry(request);
            if (entry.State == EntityState.Detached)
                await _context.Requests.AddAsync(request);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

    }


}
