using GroceryShopAPI.Models;
using GroceryShopAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Services
{
    public class AisleService
    {
        private readonly IAisleRepository _aisleRepository;
        private readonly IProductRepository _productRepository; // Declare the Product Repository

        public AisleService(IAisleRepository aisleRepository, IProductRepository productRepository)
        {
            _aisleRepository = aisleRepository;
            _productRepository = productRepository; // Initialize the Product Repository
        }

        public Task<IEnumerable<Aisle>> GetAllAsync() =>
            _aisleRepository.GetAllAsync();

        public Task<Aisle> GetByIdAsync(string id) =>
            _aisleRepository.GetByIdAsync(id);

        public Task CreateAsync(Aisle aisle) =>
            _aisleRepository.CreateAsync(aisle);

        public Task UpdateAsync(string id, Aisle aisle) =>
            _aisleRepository.UpdateAsync(id, aisle);

        public Task DeleteAsync(string id) =>
            _aisleRepository.DeleteAsync(id);

        // List all products that belong to a specific aisle
        public async Task<IEnumerable<Product>> GetProductsByAisleAsync(string aisleId)
        {
            // Fetch products by aisleId using the product repository
            return await _productRepository.GetProductsByAisleAsync(aisleId);
        }
    }
}
