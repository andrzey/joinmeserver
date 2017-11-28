using System.Net.Http;
using System.Threading.Tasks;
using joinmeserver.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        [HttpPost("login")]
        public async Task<IActionResult> LoginWithFacebook([FromBody]FacebookToken token)
        {
            var user = await GetFacebookUser(token.AccessToken);
            if (user == null)
                return NotFound();
            
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
                    FacebookId = json["id"].ToString(),
                    FirstName = json["first_name"].ToString(),
                };

                return user;
            }
        }
    }
}
