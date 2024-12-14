using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.DTOs.Package
{
	public class PackageUpdateRequest
	{
		public Guid PackageId { get; set; }
		public string PackageName { get; set; } = string.Empty;
		public int Credits { get; set; }
		public decimal Price { get; set; }
		public int ExpiryInDays { get; set; }
		public string Country { get; set; } = string.Empty;
	}
}
