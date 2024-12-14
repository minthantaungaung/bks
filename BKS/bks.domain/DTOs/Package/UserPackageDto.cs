using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.DTOs.Package
{
	public class UserPackageDto
	{
		public Guid PackageId { get; set; }
		public string PackageName { get; set; }
		public int RemainingCredits { get; set; }
		public DateTime ExpiryDate { get; set; }
		public bool IsExpired { get; set; }
	}
}
