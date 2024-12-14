using bks.domain.DTOs.Package;
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
    public class PackageService : IPackageService
	{
		private readonly IPackageRepository _packageRepository;

		public PackageService(IPackageRepository packageRepository)
		{
			_packageRepository = packageRepository;
		}

		public async Task<IEnumerable<PackageResponse>> GetAllPackagesAsync()
		{
			var packages = await _packageRepository.GetAllAsync();
			return packages.Select(p => new PackageResponse
			{
				PackageId = p.PackageId,
				PackageName = p.PackageName,
				Credits = p.Credits,
				Price = p.Price,
				ExpiryInDays = p.ExpiryInDays,
				Country = p.Country,
				CreatedAt = p.CreatedAt,
				UpdatedAt = p.UpdatedAt
			});
		}

		public async Task<PackageResponse> GetPackageByIdAsync(Guid id)
		{
			var package = await _packageRepository.GetByIdAsync(id);
			if (package == null)
			{
				throw new KeyNotFoundException("Package not found.");
			}

			return new PackageResponse
			{
				PackageId = package.PackageId,
				PackageName = package.PackageName,
				Credits = package.Credits,
				Price = package.Price,
				ExpiryInDays = package.ExpiryInDays,
				Country = package.Country,
				CreatedAt = package.CreatedAt,
				UpdatedAt = package.UpdatedAt
			};
		}

		public async Task CreatePackageAsync(PackageCreateRequest request)
		{
			var package = new Package
			{
				PackageName = request.PackageName,
				Credits = request.Credits,
				Price = request.Price,
				ExpiryInDays = request.ExpiryInDays,
				Country = request.Country
			};

			await _packageRepository.AddAsync(package);
		}

		public async Task UpdatePackageAsync(PackageUpdateRequest request)
		{
			var package = await _packageRepository.GetByIdAsync(request.PackageId);
			if (package == null)
			{
				throw new KeyNotFoundException("Package not found.");
			}

			package.PackageName = request.PackageName;
			package.Credits = request.Credits;
			package.Price = request.Price;
			package.ExpiryInDays = request.ExpiryInDays;
			package.Country = request.Country;
			package.UpdatedAt = DateTime.UtcNow;

			await _packageRepository.UpdateAsync(package);
		}

		public async Task DeletePackageAsync(Guid id)
		{
			await _packageRepository.DeleteAsync(id);
		}
		public async Task<List<PackageDto>> GetAvailablePackagesByCountryAsync(string country)
		{
			var packages = await _packageRepository.GetAvailablePackagesByCountryAsync(country);
			return packages.Select(p => new PackageDto
			{
				PackageId = p.PackageId,
				PackageName = p.PackageName,
				Credits = p.Credits,
				Price = p.Price,
				ExpiryInDays = p.ExpiryInDays,
				Country = p.Country
			}).ToList();
		}

		public async Task<List<UserPackageDto>> GetUserPurchasedPackagesAsync(Guid userId)
		{
			var purchasedPackages = await _packageRepository.GetUserPurchasedPackagesAsync(userId);

			return purchasedPackages.Select(pp => new UserPackageDto
			{
				PackageId = pp.Package.PackageId,
				PackageName = pp.Package.PackageName,
				RemainingCredits = pp.RemainingCredits,
				ExpiryDate = pp.ExpiryDate,
				IsExpired = pp.ExpiryDate < DateTime.UtcNow
			}).ToList();
		}
	}

}
