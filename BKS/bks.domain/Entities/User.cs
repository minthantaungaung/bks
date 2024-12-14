using bks.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BKS.Models
{
	public class User
	{
		[Key]
		public Guid UserId { get; set; } = Guid.NewGuid();
		public string Email { get; set; } = string.Empty;
		public string PasswordHash { get; set; } = string.Empty;
		public string FullName { get; set; } = string.Empty;
		public bool IsEmailVerified { get; set; } = false;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public int Credits { get; set; }

		public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
		public ICollection<PurchasedPackages> PurchasedPackages { get; set; } = new List<PurchasedPackages>();
		public ICollection<Waitlist> Waitlists { get; set; } = new List<Waitlist>();
	}


}
