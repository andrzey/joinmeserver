using System;
using joinmeserver.Context;
using joinmeserver.Models;
using Microsoft.Extensions.Options;

namespace joinmeserver.Repository
{
    public class UserRepository
    {
        private readonly UserContext _context = null;

        public UserRepository(IOptions<Settings> settings)
        {
            _context = new UserContext(settings);
        }
    }
}
