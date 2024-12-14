using System.ComponentModel.DataAnnotations;

namespace BKS.Models
{
	public class ClassSchedule
	{
		[Key]
		public Guid ScheduleId { get; set; } = Guid.NewGuid();
		public string ClassName { get; set; } = string.Empty;
		public int RequiredCredits { get; set; }
		public string Country { get; set; } = string.Empty;
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }
		public int MaxSlots { get; set; }
		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
		public ICollection<Waitlist> Waitlists { get; set; } = new List<Waitlist>();
	}
}
