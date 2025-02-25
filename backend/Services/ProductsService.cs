using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using GameStore.Models;
using GameStore.Models.Dto;
using GameStore.Models.Request;
using GameStore.Utils;
using MongoDB.Driver;

namespace GameStore.Services
{
    public class ProductsService
    {
        private readonly ILogger<ProductsService> _logger;
        private readonly IMongoCollection<Product> _collection;
        private readonly Cloudinary _cloudinary;


        public ProductsService(IMongoDatabase database, ILogger<ProductsService> logger)
        {
            _logger = logger;
            _collection = database.GetCollection<Product>("Products");
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
        }


        public async Task<ProductDto> Create(ProductRequest product)
        {
            var newProduct = new Product
            {
                CreationDate = DateTime.UtcNow,
            };

            if (product.Title != null) newProduct.Title = product.Title;
            if (product.Game != null) newProduct.Game = product.Game;
            if (product.Price != null) newProduct.Price = (double)product.Price;
            if (product.IsAviable != null) newProduct.IsAviable = (bool)product.IsAviable;
            if (product.Img != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(product.Img.FileName, product.Img.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "products/"
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Created product picture: " + uploadResult.JsonObj);

                var transformedUrl = AppUtils.GetTransformationUrl(
                    uploadResult.SecureUrl.ToString(),
                    "c_thumb,h_200,w_200"
                );

                newProduct.Img = transformedUrl;
                newProduct.ImgId = uploadResult.PublicId;
            }

            await _collection.InsertOneAsync(newProduct);

            return newProduct.ToProductDto();
        }


        public async Task<List<ProductDto>> GetAll()
        {
            var products = await _collection.Find(_ => true).ToListAsync();
            return products.Select(product => product.ToProductDto()).ToList();
        }


        public async Task<ProductDto> GetById(string id)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, id);
            var product = await _collection.Find(filter).FirstOrDefaultAsync();

            if (product == null) return null;
            return product.ToProductDto();
        }


        public async Task<DeleteResult> DeleteById(string id)
        {
            var existProduct = await _collection.Find(
                Builders<Product>.Filter.Eq(u => u.Id, id))
                    .FirstOrDefaultAsync();

            if (existProduct is null) return null;

            if (existProduct.ImgId != null)
            {
                var deletionParams = new DeletionParams(existProduct.ImgId);

                var deletionResult = _cloudinary.Destroy(deletionParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Removed product picture: " + deletionResult.JsonObj);
            }

            var filter = Builders<Product>.Filter.Eq(u => u.Id, id);
            return await _collection.DeleteOneAsync(_ => true);
        }


        public async Task<ProductDto> Update(ProductRequest product)
        {
            var filter = Builders<Product>.Filter.Eq(u => u.Id, product.Id);
            var existProduct = await _collection.Find(
                Builders<Product>.Filter.Eq(u => u.Id, product.Id))
                    .FirstOrDefaultAsync();

            if (existProduct is null) return null;

            var updateDefinitions = new List<UpdateDefinition<Product>>();

            if (product.Title != null)
            {
                existProduct.Title = product.Title;
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Title, product.Title));
            }
            if (product.Game != null)
            {
                existProduct.Game = product.Game;
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Game, product.Game));
            }
            if (product.Price != null)
            {
                existProduct.Price = (double)product.Price;
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Price, product.Price));
            }
            if (product.IsAviable != null)
            {
                existProduct.IsAviable = (bool)product.IsAviable;
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.IsAviable, product.IsAviable));
            }

            if (product.Img != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(product.Img.FileName, product.Img.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "products/"
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Updated product picture: " + uploadResult.JsonObj);

                var transformedUrl = AppUtils.GetTransformationUrl(
                    uploadResult.SecureUrl.ToString(),
                    "c_thumb,h_200,w_200"
                );

                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.Img, transformedUrl));
                updateDefinitions.Add(Builders<Product>.Update.Set(p => p.ImgId, uploadResult.PublicId));

                if (existProduct.ImgId != null)
                {
                    var deletionParams = new DeletionParams(existProduct.ImgId);

                    var deletionResult = _cloudinary.Destroy(deletionParams);

                    /* ==== Logger ==== */
                    _logger.LogInformation("Old product picture: " + deletionResult.JsonObj);
                }

                existProduct.Img = transformedUrl;
                existProduct.ImgId = uploadResult.PublicId;
            }

            var update = Builders<Product>.Update.Combine(updateDefinitions);
            await _collection.UpdateOneAsync(filter, update);
            return existProduct.ToProductDto();
        }
    }
}