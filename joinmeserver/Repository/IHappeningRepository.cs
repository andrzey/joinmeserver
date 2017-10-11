using System.Collections.Generic;
using System.Threading.Tasks;
using joinmeserver.Models;

namespace joinmeserver.Repository
{
    public interface IHappeningRepository
    {
        Task<IEnumerable<Happening>> GetAllHappenings();
        Task<Happening> GetHappening(string id);
        Task AddHappening(Happening item);
    }
}
