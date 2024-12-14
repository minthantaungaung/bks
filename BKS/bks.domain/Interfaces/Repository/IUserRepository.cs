using bks.domain.Entities;
using BKS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bks.domain.Interfaces.Repository
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
        Task SaveChangesAsync();
        Task UpdateAsync(User user);
        Task<PurchasedPackages> GetPurchaseByUserIdAsync(Guid userId);

	}
}
