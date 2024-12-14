using bks.domain.DTOs.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Service
{
	public interface IPackageService
	{
		Task<IEnumerable<PackageResponse>> GetAllPackagesAsync();
		Task<PackageResponse> GetPackageByIdAsync(Guid id);
		Task CreatePackageAsync(PackageCreateRequest request);
		Task UpdatePackageAsync(PackageUpdateRequest request);
		Task DeletePackageAsync(Guid id);
		Task<List<PackageDto>> GetAvailablePackagesByCountryAsync(string country);
		Task<List<UserPackageDto>> GetUserPurchasedPackagesAsync(Guid userId);
	}

}
