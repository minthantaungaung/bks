//using bks.domain.DTOs.Bookings;
//using bks.domain.Interfaces;
//using bks.domain.Interfaces.Repository;
//using bks.domain.Interfaces.Service;
//using BKS.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace bks.inftasturcture.Service
//{
//	public class BookingService : IBookingService
//	{
//		private readonly IUserRepository _userRepository;
//		private readonly IClassScheduleRepository _scheduleRepository;
//		private readonly ICacheService _cacheService;

//		public BookingService(IUserRepository userRepository, IClassScheduleRepository scheduleRepository, ICacheService cacheService)
//		{
//			_userRepository = userRepository;
//			_scheduleRepository = scheduleRepository;
//			_cacheService = cacheService;
//		}

//		public async Task<BookClassResult> BookClassAsync(Guid userId, Guid scheduleId)
//		{
//			var user = await _userRepository.GetByIdAsync(userId);
//			var purchase = await _userRepository.GetPurchaseByUserIdAsync(userId);
//			var schedule = await _scheduleRepository.GetScheduleByIdAsync(scheduleId);

//			// Check if the user can book the class
//			if (purchase.RemainingCredits < schedule.RequiredCredits)
//			{
//				return BookClassResult.Failure("Insufficient credits");
//			}

//			// Prevent overlapping bookings
//			var cacheKey = $"Booking-{userId}-{scheduleId}";
//			if (await _cacheService.ExistsAsync(cacheKey))
//			{
//				return BookClassResult.Failure("Booking conflict");
//			}

//			// Reserve slot in cache to prevent overbooking
//			await _cacheService.SetAsync(cacheKey, true, TimeSpan.FromMinutes(5));

//			var booking = new Booking
//			{
//				UserId = userId,
//				ClassScheduleId = scheduleId
//			};

//			await _scheduleRepository.BookClassAsync(booking);
//			purchase.RemainingCredits -= schedule.RequiredCredits;

//			return BookClassResult.Success(booking);
//		}

//		public async Task CancelBookingAsync(Guid bookingId, Guid userId)
//		{
//			var booking = await _scheduleRepository.GetBookingByIdAsync(bookingId);
//			if (booking != null && booking.UserId == userId)
//			{
//				await _scheduleRepository.CancelBookingAsync(bookingId);

//				// If within 4 hours, no refund is given
//				if ((booking.CreatedAt - DateTime.UtcNow).TotalHours > 4)
//				{
//					var schedule = await _scheduleRepository.GetScheduleByIdAsync(booking.ClassScheduleId);
//					var purchase = await _userRepository.GetPurchaseByUserIdAsync(userId);
//					purchase.RemainingCredits += schedule.RequiredCredits;
//				}
//			}
//		}

//		public async Task CheckInAsync(Guid bookingId, Guid userId)
//		{
//			var booking = await _scheduleRepository.GetBookingByIdAsync(bookingId);
//			if (booking != null && booking.UserId == userId)
//			{
//				booking.CheckedIn = true;
//				await _scheduleRepository.UpdateBookingAsync(bookingId,userId, false);
//			}
//		}
//	}

//}
