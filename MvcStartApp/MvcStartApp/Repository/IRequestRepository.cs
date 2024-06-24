using MvcStartApp.Models.Db;
using System.Threading.Tasks;

namespace MvcStartApp.Repository
{
    
    public interface IRequestRepository
    {
        Task AddRequest(Request request);


    }


}
