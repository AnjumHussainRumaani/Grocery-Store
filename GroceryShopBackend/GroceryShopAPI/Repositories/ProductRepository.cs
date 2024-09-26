using GroceryShopAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly IMongoCollection<Product> _products;

        public ProductRepository(IMongoDatabase database)
        {
            _products = database.GetCollection<Product>("Products");
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _products.Find(_ => true).ToListAsync();

        public async Task<Product> GetByIdAsync(string id) =>
            await _products.Find(p => p.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Product product) =>
            await _products.InsertOneAsync(product);

        public async Task UpdateAsync(string id, Product product) =>
            await _products.ReplaceOneAsync(p => p.Id == new Guid(id), product);

        public async Task DeleteAsync(string id) =>
            await _products.DeleteOneAsync(p => p.Id == new Guid(id));

        // Updated method to match the interface
        public async Task<IEnumerable<Product>> GetProductsByAisleAsync(string aisleId)
        {
            // Convert the string ID to Guid
            var aisleGuid = new Guid(aisleId);

            // Query to find products by aisle ID
            var filter = Builders<Product>.Filter.Eq(p => p.AisleId, aisleGuid);
            return await _products.Find(filter).ToListAsync();
        }
    }
}
