using Microsoft.AspNetCore.Mvc;
using joinmeserver.Repository;
using joinmeserver.Models;
using System;
using System.Threading.Tasks;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class HappeningController : Controller
    {
        private readonly HappeningRepository _happeningRepository;

        public HappeningController(HappeningRepository happeningRepository)
        {
            _happeningRepository = happeningRepository;
        }

        [HttpPost]
        public async Task<IActionResult> AddHappening([FromBody]Happening happening)
        {
            if (happening == null)
            {
                return BadRequest("Happening is missing or deserilization failed");
            }

            var newHappening = new Happening
            {
                Id = new Guid(),
                Name = happening.Name,
                Place = happening.Place
            };

            await _happeningRepository.AddHappening(newHappening);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetHappeningList()
        {
            var happenings = await _happeningRepository.GetAllHappenings();

            return Json(happenings);
        }
    }
}
