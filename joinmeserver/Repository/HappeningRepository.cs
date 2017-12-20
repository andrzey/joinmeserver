using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using joinmeserver.Context;
using joinmeserver.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace joinmeserver.Repository
{
    public class HappeningRepository
    {
        private readonly HappeningContext _context;

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

        public async Task<Happening> GetHappeningById(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            var happening = await _context.Happenings.Find(h => h.Id == id).SingleAsync();

            return happening;
        }

        public async Task<bool> DeleteHappening(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException(nameof(id));

            var deleteResult = await _context.Happenings.DeleteOneAsync(h => h.Id == id);

            return deleteResult.IsAcknowledged;
        }

        public async Task<List<Happening>> GetHappeningsUserCreated(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var happenings = await _context.Happenings.Find(h => h.CreatedByUser == userId).ToListAsync();

            return happenings;
        }

        public async Task<List<Happening>> GetHappeningsUserIsAttending(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var filter = Builders<Happening>.Filter.ElemMatch(h => h.Users, h => h.FacebookId == userId);
            var happenings = await _context.Happenings.Find(filter).ToListAsync();

            return happenings;
        }

        public async Task<Happening> AddCommentToHappening(Guid happeningId, Comment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            if (happeningId == Guid.Empty) throw new ArgumentNullException(nameof(happeningId));

            var update = Builders<Happening>.Update.Push("Comments", comment);

            var result = await _context.Happenings.FindOneAndUpdateAsync(h => h.Id == happeningId, update);

            return result;
        }
    }
}
