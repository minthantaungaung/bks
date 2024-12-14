using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.DTOs.Bookings
{
	public class BookClassResult
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }
		public Booking Data { get; set; }

		public static BookClassResult Success(Booking booking)
		{
			return new BookClassResult { IsSuccess = true, Data = booking };
		}

		public static BookClassResult Failure(string message)
		{
			return new BookClassResult { IsSuccess = false, Message = message };
		}
	}

}
