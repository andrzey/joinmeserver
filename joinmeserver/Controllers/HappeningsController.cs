using Microsoft.AspNetCore.Mvc;
using joinmeserver.Repository;
using joinmeserver.Models;
using System;
using System.Threading.Tasks;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class HappeningsController : Controller
    {
        private readonly HappeningRepository _happeningRepository;

        public HappeningsController(HappeningRepository happeningRepository)
        {
            _happeningRepository = happeningRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHappening([FromBody]Happening happening)
        {
            if (happening == null)
            {
                return BadRequest("Happening is missing or deserilization failed");
            }

            var newHappening = new Happening
            {
                Id = new Guid(),
                CreatedByUser = happening.CreatedByUser,
                Name = happening.Name,
                Place = happening.Place
            };

            await _happeningRepository.AddHappening(newHappening);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHappening(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            var result = await _happeningRepository.DeleteHappening(id);

            if (!result) return NotFound();

            return StatusCode(204);
        }

        [HttpGet]
        public async Task<IActionResult> GetHappeningList()
        {
            var happenings = await _happeningRepository.GetAllHappenings();

            return Ok(happenings);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHappening(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));

            var happening = await _happeningRepository.GetHappening(id);

            if (happening == null)
                return NotFound(id);

            return Ok(happening);
        }
    }
}
