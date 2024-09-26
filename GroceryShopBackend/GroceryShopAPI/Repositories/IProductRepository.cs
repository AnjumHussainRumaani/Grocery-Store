using GroceryShopAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(string id);
        Task CreateAsync(Product product);
        Task UpdateAsync(string id, Product product);
        Task DeleteAsync(string id);

        // New method to get products by aisle
        Task<IEnumerable<Product>> GetProductsByAisleAsync(string aisleId);
    }
}
