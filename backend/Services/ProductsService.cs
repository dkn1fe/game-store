using GameStore.Models;
using GameStore.Models.Request;
using GameStore.Utils;
using MongoDB.Driver;

namespace GameStore.Services
{
    public class ProductsService
    {
        private readonly IMongoCollection<Product> _collection;

        public ProductsService( IMongoDatabase database)
        {
            _collection = database.GetCollection<Product>("Products");
        }


        public async Task Create(Product product)
        {
            product.CreationDate = DateTime.UtcNow;

            await _collection.InsertOneAsync(product);
        }


        public async Task<List<Product>> BulkCreate(List<Product> products)
        {
            foreach (var product in products)
            {
                product.CreationDate = DateTime.UtcNow;
            }

            await _collection.InsertManyAsync(products);
            return products;
        }


        public async Task<List<Product>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }


        public async Task<Product> GetById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<DeleteResult> DeleteAll()
        {
            return await _collection.DeleteManyAsync(_ => true);
        }


        public async Task<DeleteResult> DeleteById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, id);
            return await _collection.DeleteOneAsync(_ => true);

        }


        public async Task<Dictionary<string, object>> Update(ProductRequest product) // updatePutMethod
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, product.Id);

            var updateDefinitions = new List<UpdateDefinition<Product>>();
            var updateFields = new Dictionary<string, object>();

            if (product.Title != null)
            {
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Title, product.Title));
                updateFields.Add(AppUtils.FirstCharLow(nameof(Product.Title)), product.Title);
            }

            if (product.Game != null)
            {
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Game, product.Game));
                updateFields.Add(AppUtils.FirstCharLow(nameof(Product.Game)), product.Game);
            }

            if (product.Price != null)
            {
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Price, product.Price));
                updateFields.Add(AppUtils.FirstCharLow(nameof(Product.Price)), product.Price);
            }

            if (product.Img != null)
            {
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Img, product.Img));
                updateFields.Add(AppUtils.FirstCharLow(nameof(Product.Img)), product.Img);
            }

            if (product.IsAviable != null)
            {
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.IsAviable, product.IsAviable));
                updateFields.Add(AppUtils.FirstCharLow(nameof(Product.IsAviable)), product.IsAviable);
            }


            var update = Builders<Product>.Update.Combine(updateDefinitions);
            await _collection.UpdateOneAsync(filter, update);
            return updateFields;
        }
    }
}