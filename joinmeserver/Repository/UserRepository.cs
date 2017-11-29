using System;
using System.Threading.Tasks;
using joinmeserver.Context;
using joinmeserver.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace joinmeserver.Repository
{
    public class UserRepository
    {
        private readonly UserContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task<User> GetUserByFacebookId(string facebookId)
        {
            if (String.IsNullOrEmpty(facebookId)) throw new ArgumentNullException(nameof(facebookId));

            try
            {
                var user = await _context.Users.Find(u => u.FacebookId == facebookId).FirstOrDefaultAsync();

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
