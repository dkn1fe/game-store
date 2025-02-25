using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using GameStore.Models;
using GameStore.Models.Requests;
using GameStore.Utils;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using GameStore.Models.Dto;

namespace GameStore.Services
{
    public class UserService
    {
        private readonly ILogger<ProductsService> _logger;
        private readonly IMongoCollection<User> _collection;
        private readonly Cloudinary _cloudinary;


        public UserService(IMongoDatabase database, ILogger<ProductsService> logger)
        {
            _logger = logger;
            _collection = database.GetCollection<User>("Users");
            _cloudinary = new Cloudinary(Environment.GetEnvironmentVariable("CLOUDINARY_URL"));
            _cloudinary.Api.Secure = true;
        }


        public async Task<bool> UserExists(UserRequest user)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Username, user.Username),
                Builders<User>.Filter.Eq(u => u.Email, user.Email)
            );

            var existingUser = await _collection.Find(filter).FirstOrDefaultAsync();
            return existingUser != null;
        }


        public async Task<User> GetByUsernameOrEmail(string usernameOrEmail)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Username, usernameOrEmail),
                Builders<User>.Filter.Eq(u => u.Email, usernameOrEmail)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<UserDto> Register(UserRequest user)
        {
            if (await UserExists(user)) return null;

            var newUser = new User
            {
                CreationDate = DateTime.UtcNow,
            };

            if (user.Username != null) newUser.Username = user.Username;
            if (user.Password != null) newUser.Password = user.Password;
            if (user.Email != null) newUser.Email = user.Email;
            if (user.Role != null) newUser.Role = user.Role;
            if (user.Img != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(user.Img.FileName, user.Img.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "users/"
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Created user picture: " + uploadResult.JsonObj);

                var transformedUrl = AppUtils.GetTransformationUrl(
                    uploadResult.SecureUrl.ToString(),
                    "c_thumb,g_face,h_200,w_200/r_max/f_auto"
                );

                newUser.Img = transformedUrl;
                newUser.ImgId = uploadResult.PublicId;
            }

            await _collection.InsertOneAsync(newUser);
            return newUser.ToUserDto();
        }


        public async Task<List<UserDto>> GetAll()
        {
            var users = await _collection.Find(_ => true).ToListAsync();
            return users.Select(user => user.ToUserDto()).ToList();

        }


        public async Task<UserDto> GetById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            var user = await _collection.Find(filter).FirstOrDefaultAsync();

            if (user == null) return null;
            return user.ToUserDto();
        }


        public async Task<DeleteResult> DeleteById(string id)
        {
            var existUser = await _collection.Find(
                Builders<User>.Filter.Eq(u => u.Id, id))
                    .FirstOrDefaultAsync();

            if (existUser is null) return null;

            if (existUser.ImgId != null)
            {
                var deletionParams = new DeletionParams(existUser.ImgId);

                var deletionResult = _cloudinary.Destroy(deletionParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Removed user picture: " + deletionResult.JsonObj);
            }

            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _collection.DeleteOneAsync(_ => true);
        }


        public async Task<UserDto> Update(UserRequest user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);

            var existUser = await _collection.Find(
                Builders<User>.Filter.Eq(u => u.Id, user.Id))
                    .FirstOrDefaultAsync();

            if (existUser is null) return null;

            var updateDefinitions = new List<UpdateDefinition<User>>();

            if (user.Username != null)
            {
                existUser.Username = user.Username;
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Username, user.Username));
            }
            if (user.Email != null)
            {
                existUser.Email = user.Email;
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Email, user.Email));
            }
            if (user.Role != null)
            {
                existUser.Role = user.Role;
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Role, user.Role));
            }
            if (user.Img != null)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(user.Img.FileName, user.Img.OpenReadStream()),
                    UseFilename = true,
                    UniqueFilename = true,
                    Folder = "users/"
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                /* ==== Logger ==== */
                _logger.LogInformation("Updated user picture: " + uploadResult.JsonObj);

                var transformedUrl = AppUtils.GetTransformationUrl(
                    uploadResult.SecureUrl.ToString(),
                    "c_thumb,g_face,h_200,w_200/r_max/f_auto"
                );

                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Img, transformedUrl));
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.ImgId, uploadResult.PublicId));

                if (existUser.ImgId != null)
                {
                    var deletionParams = new DeletionParams(existUser.ImgId);

                    var deletionResult = _cloudinary.Destroy(deletionParams);

                    /* ==== Logger ==== */
                    _logger.LogInformation("Old user picture: " + deletionResult.JsonObj);
                }

                existUser.Img = transformedUrl;
                existUser.ImgId = uploadResult.PublicId;
            }

            var update = Builders<User>.Update.Combine(updateDefinitions);
            await _collection.UpdateOneAsync(filter, update);
            return existUser.ToUserDto();
        }
    }
}
