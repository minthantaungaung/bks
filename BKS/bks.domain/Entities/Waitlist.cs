
using System.ComponentModel.DataAnnotations;

namespace BKS.Models
{
	public class Waitlist
	{
		[Key]
		public Guid WaitlistId { get; set; } = Guid.NewGuid();
		public Guid UserId { get; set; }
		public Guid ScheduleId { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public string Status { get; set; }

		public User User { get; set; }
		public ClassSchedule Schedule { get; set; }
	}
}
