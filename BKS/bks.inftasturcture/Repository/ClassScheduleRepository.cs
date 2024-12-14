using bks.domain.Data;
using bks.domain.Interfaces;
using bks.domain.Interfaces.Repository;
using BKS.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.inftasturcture.Repository
{
	public class ClassScheduleRepository : IClassScheduleRepository
	{
		private readonly BookingDbContext _context;

		public ClassScheduleRepository(BookingDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<ClassSchedule>> GetAllClassSchedules(string country)
		{
			return _context.ClassSchedules
				.Where(s => s.Country == country)
				.ToList();
		}

		public async Task<ClassSchedule> GetClassSchedule(Guid scheduleId)
		{
			return _context.ClassSchedules.Find(scheduleId);
		}

		public async Task<Booking> BookClass(Guid userId, Guid scheduleId)
		{
			var schedule = _context.ClassSchedules.Find(scheduleId);
			if (schedule == null || schedule.MaxSlots <= schedule.Bookings.Count)
				return null; // Schedule full or not found

			var booking = new Booking
			{
				UserId = userId,
				ClassScheduleId = scheduleId,
				CreatedAt = DateTime.UtcNow,
				Status = BookingStatus.Confirmed.ToString()
			};

			_context.Bookings.Add(booking);
			_context.SaveChanges();

			return booking;
		}

		public async Task<Booking> CancelBooking(Guid bookingId, Guid userId)
		{
			var booking = _context.Bookings.Find(bookingId);
			if (booking != null && booking.UserId == userId)
			{
				booking.CanceledAt = DateTime.UtcNow;
				booking.Status = BookingStatus.Canceled.ToString();
				_context.SaveChanges();
				return booking;
			}
			return null;
		}

		public async Task<Waitlist> AddToWaitlist(Guid userId, Guid scheduleId)
		{
			var schedule = _context.ClassSchedules.Find(scheduleId);
			if (schedule == null)
				return null;

			var waitlist = new Waitlist
			{
				UserId = userId,
				ScheduleId = scheduleId,
				Status = BookingStatus.Waitlisted.ToString(),
				CreatedAt = DateTime.UtcNow
			};

			_context.Waitlists.Add(waitlist);
			_context.SaveChanges();
			return waitlist;
		}

		public async Task<Booking> CheckIn(Guid bookingId, Guid userId)
		{
			var booking = _context.Bookings.Find(bookingId);
			if (booking != null && booking.UserId == userId && booking.CheckedIn == false && DateTime.UtcNow >= booking.Schedule.StartTime)
			{
				booking.CheckedIn = true;
				_context.SaveChanges();
				return booking;
			}
			return null;
		}
	}
}
