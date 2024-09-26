using GroceryShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Repositories
{
    public interface IAisleRepository
    {
        Task<IEnumerable<Aisle>> GetAllAsync();
        Task<Aisle> GetByIdAsync(string id);
        Task CreateAsync(Aisle aisle);
        Task UpdateAsync(string id, Aisle aisle);
        Task DeleteAsync(string id);
    }
}
