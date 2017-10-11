using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using joinmeserver.Context;
using joinmeserver.Models;
using Microsoft.Extensions.Options;

namespace joinmeserver.Repository
{
    public class HappeningRepository: IHappeningRepository
    {
        private readonly HappeningContext _context = null;

        public HappeningRepository(IOptions<Settings> settings)
        {
            _context = new HappeningContext(settings);
        }

        public async Task AddHappening(Happening item)
        {
            await _context.Happenings.InsertOneAsync(item);

        }

        public Task<IEnumerable<Happening>> GetAllHappenings()
        {
            throw new NotImplementedException();
        }

        public Task<Happening> GetHappening(string id)
        {
            throw new NotImplementedException();
        }
    }
}
