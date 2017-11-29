using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using joinmeserver.Context;
using joinmeserver.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace joinmeserver.Repository
{
    public class HappeningRepository
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

        public async Task<IEnumerable<Happening>> GetAllHappenings()
        {
            List<Happening> happenings;

            try
            {
                happenings = await _context.Happenings.Find(FilterDefinition<Happening>.Empty).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

            return happenings.Select(item => new Happening 
            {
                Id = item.Id,
                Name = item.Name,
                Place = item.Place
            });

        }

        public Task<Happening> GetHappening(string id)
        {
            throw new NotImplementedException();
        }
    }
}
