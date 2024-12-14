using bks.domain.Entities;
using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Repository
{
	public interface IPackageRepository
	{
		Task<IEnumerable<Package>> GetAllAsync();
		Task<Package> GetByIdAsync(Guid id);
		Task AddAsync(Package package);
		Task UpdateAsync(Package package);
		Task DeleteAsync(Guid id);
		Task<List<Package>> GetAvailablePackagesByCountryAsync(string country);
		Task<List<PurchasedPackages>> GetUserPurchasedPackagesAsync(Guid userId);
	}

}
