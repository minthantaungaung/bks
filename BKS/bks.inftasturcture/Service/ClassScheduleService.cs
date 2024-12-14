using bks.domain.Interfaces.Repository;
using bks.domain.Interfaces.Service;
using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.inftasturcture.Service
{
	public class ClassScheduleService : IClassScheduleService
	{
		private readonly IClassScheduleRepository _repository;

		public ClassScheduleService(IClassScheduleRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<ClassSchedule>> GetAllClassSchedules(string country)
		{
			return await _repository.GetAllClassSchedules(country);
		}

		public async Task<ClassSchedule> GetClassSchedule(Guid scheduleId)
		{
			return await _repository.GetClassSchedule(scheduleId);
		}

		public async Task<Booking> BookClass(Guid userId, Guid scheduleId)
		{
			return await _repository.BookClass(userId, scheduleId);
		}

		public async Task<Booking> CancelBooking(Guid bookingId, Guid userId)
		{
			return await _repository.CancelBooking(bookingId, userId);
		}

		public async Task<Waitlist> AddToWaitlist(Guid userId, Guid scheduleId)
		{
			return await _repository.AddToWaitlist(userId, scheduleId);
		}

		public async Task<Booking> CheckIn(Guid bookingId, Guid userId)
		{
			return await _repository.CheckIn(bookingId, userId);
		}
	}
}
