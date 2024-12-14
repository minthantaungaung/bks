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
    public class PackageRepository : IPackageRepository
	{
		private readonly BookingDbContext _context;

		public PackageRepository(BookingDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<Package>> GetAllAsync()
		{
			return await _context.Packages.ToListAsync();
		}

		public async Task<Package> GetByIdAsync(Guid id)
		{
			return await _context.Packages.FindAsync(id);
		}

		public async Task AddAsync(Package package)
		{
			await _context.Packages.AddAsync(package);
			await _context.SaveChangesAsync();
		}

		public async Task UpdateAsync(Package package)
		{
			_context.Packages.Update(package);
			await _context.SaveChangesAsync();
		}

		public async Task DeleteAsync(Guid id)
		{
			var package = await _context.Packages.FindAsync(id);
			if (package != null)
			{
				_context.Packages.Remove(package);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<List<Package>> GetAvailablePackagesByCountryAsync(string country)
		{
			return await _context.Packages
				.Where(p => p.Country == country)
				.OrderBy(p => p.PackageName)
				.ToListAsync();
		}

		public async Task<List<PurchasedPackages>> GetUserPurchasedPackagesAsync(Guid userId)
		{
			return default;
		}
	}


}
