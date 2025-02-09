using MongoDB.Driver;
using GameStore.Models;

namespace GameStore.Infrastructure
{
    public class ProductRepository
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("Products");
        }

        public async Task Create(Product product)
        {
            await _collection.InsertOneAsync(product);
        }

        public async Task BulkCreate(List<Product> products)
        {
            await _collection.InsertManyAsync(products);
        }

        public async Task<List<Product>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }
        
        public async Task<Product> GetProductById(string id)
        {
            var filter = Builders<Product>.Filter.Eq("_id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }
        
        public async Task<DeleteResult> DeleteAll()
        {
            return await _collection.DeleteManyAsync(_ => true);
        }
    }
}
