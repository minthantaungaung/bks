using bks.domain.DTOs.Package;
using bks.domain.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace bks.api.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	[AllowAnonymous]
	public class PackageController : ControllerBase
	{
		private readonly IPackageService _packageService;

		public PackageController(IPackageService packageService)
		{
			_packageService = packageService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllPackages()
		{
			var packages = await _packageService.GetAllPackagesAsync();
			return Ok(packages);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetPackageById(Guid id)
		{
			try
			{
				var package = await _packageService.GetPackageByIdAsync(id);
				return Ok(package);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { Message = ex.Message });
			}
		}

		[HttpPost]
		public async Task<IActionResult> CreatePackage([FromBody] PackageCreateRequest request)
		{
			await _packageService.CreatePackageAsync(request);
			return StatusCode(201);
		}

		[HttpPut]
		public async Task<IActionResult> UpdatePackage([FromBody] PackageUpdateRequest request)
		{
			try
			{
				await _packageService.UpdatePackageAsync(request);
				return NoContent();
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new { Message = ex.Message });
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeletePackage(Guid id)
		{
			await _packageService.DeletePackageAsync(id);
			return NoContent();
		}
		[HttpGet("available/{country}")]
		public async Task<ActionResult<List<PackageDto>>> GetAvailablePackagesByCountry(string country)
		{
			var packages = await _packageService.GetAvailablePackagesByCountryAsync(country);
			return Ok(packages);
		}
	}
}
