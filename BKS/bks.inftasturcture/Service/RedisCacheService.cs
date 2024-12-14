using bks.domain.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.inftasturcture.Service
{
	public class RedisCacheService : ICacheService
	{
		private readonly IDistributedCache _cache;

		public RedisCacheService(IDistributedCache cache)
		{
			_cache = cache;
		}

		public async Task<bool> ExistsAsync(string key)
		{
			return await _cache.GetStringAsync(key) != null;
		}

		public async Task SetAsync(string key, object value, TimeSpan expiration)
		{
			await _cache.SetStringAsync(key, JsonConvert.SerializeObject(value), new DistributedCacheEntryOptions
			{
				AbsoluteExpirationRelativeToNow = expiration
			});
		}

		public async Task RemoveAsync(string key)
		{
			await _cache.RemoveAsync(key);
		}
	}

}
