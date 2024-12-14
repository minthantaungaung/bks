using bks.domain.DTOs.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Service
{
	public interface IBookingService
	{
		Task CheckInAsync(Guid bookingId, Guid userId);
		Task CancelBookingAsync(Guid bookingId, Guid userId);
		Task<BookClassResult> BookClassAsync(Guid userId, Guid scheduleId);
	}
}
