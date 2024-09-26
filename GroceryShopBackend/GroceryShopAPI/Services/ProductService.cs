using GroceryShopAPI.Models;
using GroceryShopAPI.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<IEnumerable<Product>> GetAllAsync() =>
            _productRepository.GetAllAsync();

        public Task<Product> GetByIdAsync(string id) =>
            _productRepository.GetByIdAsync(id);

        public Task CreateAsync(Product product) =>
            _productRepository.CreateAsync(product);

        public Task UpdateAsync(string id, Product product) =>
            _productRepository.UpdateAsync(id, product);

        public Task DeleteAsync(string id) =>
            _productRepository.DeleteAsync(id);

        // New method to get products by aisle
        public Task<IEnumerable<Product>> GetProductsByAisleAsync(string aisleId) =>
            _productRepository.GetProductsByAisleAsync(aisleId);
    }
}
