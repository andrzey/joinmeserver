using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using joinmeserver.Models;
using joinmeserver.Repository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly UserRepository _userRepository;
        private readonly HappeningRepository _happeningRepository;

        public UserController(UserRepository userRepository, HappeningRepository happeningRepository)
        {
            _userRepository = userRepository;
            _happeningRepository = happeningRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginWithFacebook([FromBody]FacebookToken token)
        {
            var facebookUser = await GetFacebookUser(token.AccessToken);

            if (facebookUser == null)
                return NotFound("Facebook returned no user");

            var user = await _userRepository.GetUserByFacebookId(facebookUser.FacebookId);

            if(user == null)
            {
                await _userRepository.AddUser(facebookUser);
                return Ok(facebookUser);
            }
            
            return Ok(user);
        }

        public async Task<User> GetFacebookUser(string accessToken)
        {
            string url = $"https://graph.facebook.com/v2.9/me?fields=first_name,picture&redirect=true&access_token={accessToken}";

            using(HttpClient client = new HttpClient())
            using(HttpResponseMessage response = await client.GetAsync(url))
            using(HttpContent content = response.Content)
            {
                string result = await content.ReadAsStringAsync();

                if(result == null){
                    return null;
                }

                var json = JObject.Parse(result);
                var user = new User
                {
                    FacebookId =json["id"].ToString(),
                    FirstName = json["first_name"].ToString(),
                    Interests = new List<string>(),
                };

                return user;
            }
        }

        [HttpGet("{userId}/happenings")]
        public async Task<IActionResult> GetHappeningsForUser(string userId)
        {
            if (string.IsNullOrEmpty(userId)) throw new ArgumentNullException(nameof(userId));

            var createdHappenings = await _happeningRepository.GetHappeningsUserCreated(userId);
            var attendingHappenings = await _happeningRepository.GetHappeningsUserIsAttending(userId);

            createdHappenings.AddRange(attendingHappenings);

            return Ok(createdHappenings);
        }
    }
}
