using bks.domain.Interfaces.Service;
using BKS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace bks.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ClassScheduleController : ControllerBase
	{
		private readonly IClassScheduleService _service;

		public ClassScheduleController(IClassScheduleService service)
		{
			_service = service;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllClassSchedules([FromQuery] string country)
		{
			var schedules = await _service.GetAllClassSchedules(country);
			return Ok(schedules);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetClassSchedule(Guid id)
		{
			var schedule = await _service.GetClassSchedule(id);
			if (schedule == null)
				return NotFound();

			return Ok(schedule);
		}

		[HttpPost("book/{scheduleId}")]
		public async Task<IActionResult> BookClass(Guid scheduleId, Guid userId)
		{
			var booking = await _service.BookClass(userId, scheduleId);
			if (booking == null)
				return BadRequest("Class is full or not found");

			return Ok(booking);
		}

		[HttpPost("waitlist/{scheduleId}")]
		public async Task<IActionResult> AddToWaitlist(Guid scheduleId, Guid userId)
		{
			var waitlist = await _service.AddToWaitlist(userId, scheduleId);
			if (waitlist == null)
				return BadRequest("Can't add to waitlist");
			return Ok(waitlist);
		}

		[HttpPut("cancel/{bookingId}")]
		public async Task<IActionResult> CancelBooking(Guid bookingId, Guid userId)
		{
			var booking = await _service.CancelBooking(bookingId, userId);

			if (booking == null)
				return BadRequest("Booking cancelation failed!");
			return Ok(booking);
		}

		[HttpPut("checkin/{bookingId}")]
		public async Task<IActionResult> CheckIn(Guid bookingId, Guid userId)
		{
			var booking = await _service.CheckIn(bookingId, userId);
			if (booking == null)
				return BadRequest("Check in failed!");
			return Ok(booking);
		}
	}
}
