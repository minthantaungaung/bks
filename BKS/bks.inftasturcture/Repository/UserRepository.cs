using bks.domain.Entities;
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
    public class UserRepository : IUserRepository
	{
		private readonly BookingDbContext _context;

		public UserRepository(BookingDbContext context)
		{
			_context = context;
		}

		public async Task<User?> GetByEmailAsync(string email)
		{
			return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
		}

		public async Task<User?> GetByIdAsync(Guid id)
		{
			return await _context.Users.FindAsync(id);
		}
		public async Task UpdateAsync(User user)
		{
			_context.Set<User>().Update(user);
			await _context.SaveChangesAsync();
		}
		public async Task AddAsync(User user)
		{
			await _context.Users.AddAsync(user);
		}

		public async Task SaveChangesAsync()
		{
			await _context.SaveChangesAsync();
		}
		public async Task<PurchasedPackages> GetPurchaseByUserIdAsync(Guid userId)
		{
			//return await _context.PurchasedPackages.FirstOrDefaultAsync(up => up.UserId == userId);
			return default;
		}
	}
}
