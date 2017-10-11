using Microsoft.AspNetCore.Mvc;
using joinmeserver.Repository;
using joinmeserver.Models;
using System;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class HappeningController : Controller
    {
        private readonly IHappeningRepository _happeningRepository;

        public HappeningController(IHappeningRepository happeningRepository)
        {
            _happeningRepository = happeningRepository;
        }

        [HttpPost]
        public IActionResult AddHappening([FromBody]Happening happening)
        {
            if(happening == null)
            {
                return BadRequest("Happening is missing or deserilization failed");
            }
            
            var newHappening = new Happening
            {
                Id = new Guid(),
                Name = happening.Name,
                Place = happening.Place
            };

            _happeningRepository.AddHappening(newHappening);
            return Ok();
        }
    }
}
