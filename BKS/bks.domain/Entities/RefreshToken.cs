using BKS.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Entities
{
	public class RefreshToken
	{
		[Key]
		public Guid RefreshTokenId { get; set; } = Guid.NewGuid();
		public string Token { get; set; } = string.Empty;
		public DateTime ExpiryDate { get; set; }
		public bool IsRevoked { get; set; } = false;
		public Guid UserId { get; set; }
		public User User { get; set; }
	}
}
