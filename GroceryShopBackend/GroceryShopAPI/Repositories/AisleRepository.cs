using GroceryShopAPI.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryShopAPI.Repositories
{
    public class AisleRepository : IAisleRepository
    {
        private readonly IMongoCollection<Aisle> _aisles;

        public AisleRepository(IMongoDatabase database)
        {
            _aisles = database.GetCollection<Aisle>("Aisles");
        }

        public async Task<IEnumerable<Aisle>> GetAllAsync() =>
            await _aisles.Find(_ => true).ToListAsync();

        public async Task<Aisle> GetByIdAsync(string id) =>
            await _aisles.Find(a => a.Id == new Guid(id)).FirstOrDefaultAsync();

        public async Task CreateAsync(Aisle aisle) =>
            await _aisles.InsertOneAsync(aisle);

        public async Task UpdateAsync(string id, Aisle aisle) =>
            await _aisles.ReplaceOneAsync(a => a.Id == new Guid(id), aisle);

        public async Task DeleteAsync(string id) =>
            await _aisles.DeleteOneAsync(a => a.Id == new Guid(id));
    }
}
