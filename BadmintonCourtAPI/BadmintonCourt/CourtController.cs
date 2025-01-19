using Microsoft.AspNetCore.Mvc;
using BadmintonCourtAPI.Models;

namespace BadmintonCourtAPI.Controllers
{
    [ApiController]
    [Route("api/court")]
    public class CourtController : ControllerBase
    {
        private static List<Court> Courts = new()
        {
            new Court { Id = 1, Name = "Court A", IsAvailable = true },
            new Court { Id = 2, Name = "Court B", IsAvailable = false }
        };

        [HttpGet]
        public IActionResult GetAllCourts()
        {
            return Ok(Courts);
        }

        [HttpGet("{id}")]
        public IActionResult GetCourtById(int id)
        {
            var court = Courts.FirstOrDefault(c => c.Id == id);
            if (court == null)
                return NotFound("Court not found!");

            return Ok(court);
        }

        [HttpPost]
        public IActionResult BookCourt([FromBody] int courtId)
        {
            var court = Courts.FirstOrDefault(c => c.Id == courtId);
            if (court == null || !court.IsAvailable)
                return BadRequest("Court is not available!");

            court.IsAvailable = false;
            return Ok("Court booked successfully!");
        }

        [HttpDelete("{id}")]
        public IActionResult CancelBooking(int id)
        {
            var court = Courts.FirstOrDefault(c => c.Id == id);
            if (court == null || court.IsAvailable)
                return BadRequest("Court is not booked!");

            court.IsAvailable = true;
            return Ok("Booking cancelled!");
        }
    }

}
