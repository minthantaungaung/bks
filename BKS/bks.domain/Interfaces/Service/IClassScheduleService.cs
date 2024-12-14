using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Service
{
	public interface IClassScheduleService
	{
		Task<IEnumerable<ClassSchedule>> GetAllClassSchedules(string country);
		Task<ClassSchedule> GetClassSchedule(Guid scheduleId);
		Task<Booking> BookClass(Guid userId, Guid scheduleId);
		Task<Booking> CancelBooking(Guid bookingId, Guid userId);
		Task<Waitlist> AddToWaitlist(Guid userId, Guid scheduleId);
		Task<Booking> CheckIn(Guid bookingId, Guid userId);
	}
}
