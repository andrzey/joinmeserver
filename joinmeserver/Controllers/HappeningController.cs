using Microsoft.AspNetCore.Mvc;
using joinmeserver.Repository;
using joinmeserver.Models;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace joinmeserver.Controllers
{
    [Route("api/[controller]")]
    public class HappeningController : Controller
    {
        private readonly HappeningRepository _happeningRepository;
        private readonly UserRepository _userRepository;

        public HappeningController(HappeningRepository happeningRepository, UserRepository userRepository)
        {
            _happeningRepository = happeningRepository;
            _userRepository = userRepository;
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
            if (id == Guid.Empty) return BadRequest(nameof(id));

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
            if (id == Guid.Empty) return BadRequest(nameof(id));

            var happening = await _happeningRepository.GetHappeningById(id);

            if (happening == null)
                return NotFound(id);

            return Ok(happening);
        }

        [HttpPut("{id}/comment")]
        public async Task<IActionResult> PostComment([FromBody] Comment comment, Guid id)
        {
            if (id == Guid.Empty) return BadRequest(nameof(id));
            if (comment == null) return BadRequest(nameof(comment));
            if (string.IsNullOrEmpty(comment.Body)) return BadRequest(nameof(comment.Body));

            var result = await _happeningRepository.AddCommentToHappening(id, comment);

            if (result == null)
                return StatusCode(500);

            return Ok();
        }

        [HttpPut("{id}/attend/{userId}")]
        public async Task<IActionResult> AttendHappening(Guid id, string userId)
        {
            if (id == Guid.Empty) return BadRequest(nameof(id));
            if (string.IsNullOrEmpty(userId)) return BadRequest(nameof(userId));

            var user = await _userRepository.GetUserByFacebookId(userId);

            if (user == null)
                return NotFound("User not found");

            var attendingUser = new AttendingUser
            {
                FacebookId = user.FacebookId,
                FirstName = user.FirstName
            };

            var result = await _happeningRepository.AddUserToHappening(id, attendingUser);

            if (result == null)
                return StatusCode(500);

            return Ok();
        }
    }
}
