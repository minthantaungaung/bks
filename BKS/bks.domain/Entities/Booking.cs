
using System.ComponentModel.DataAnnotations;

namespace BKS.Models
{
	public class Booking
	{
		[Key]
		public Guid BookingId { get; set; } = Guid.NewGuid();
		public Guid UserId { get; set; }
		public Guid ClassScheduleId { get; set; }
		public bool CheckedIn { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime BookingTime { get; set; } = DateTime.UtcNow;
		public DateTime? CanceledAt { get; set; }
		public string Status { get; set; }  // Status: Booked, Canceled, Waitlisted

		public ClassSchedule Schedule { get; set; }
		public User User { get; set; }
	}

}
