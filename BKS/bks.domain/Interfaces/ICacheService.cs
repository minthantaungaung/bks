using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces
{
	public interface ICacheService
	{
		Task<bool> ExistsAsync(string key);
		Task SetAsync(string key, object value, TimeSpan expiration);
		Task RemoveAsync(string key);
	}

}
