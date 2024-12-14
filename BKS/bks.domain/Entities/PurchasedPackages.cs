using BKS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Entities
{
	public class PurchasedPackages
	{
		[Key]
		public Guid PurchasedPackageId { get; set; } = Guid.NewGuid();
		public Guid UserId { get; set; }
		public User User { get; set; }
		public Guid PackageId { get; set; }
		public Package Package { get; set; }
		public int RemainingCredits { get; set; }
		public DateTime ExpiryDate { get; set; }
		public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
	}

}
