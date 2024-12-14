using bks.domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace BKS.Models
{
	public class Package
	{
		[Key]
		public Guid PackageId { get; set; } = Guid.NewGuid();
		public string PackageName { get; set; } = string.Empty;
		public int Credits { get; set; }
		public decimal Price { get; set; }
		public int ExpiryInDays { get; set; }
		public string Country { get; set; } = string.Empty;
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
		public DateTime? UpdatedAt { get; set; }
		public ICollection<PurchasedPackages> PurchasedPackages { get; set; } = new List<PurchasedPackages>();
	}

}
