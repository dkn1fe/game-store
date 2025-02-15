using GameStore.Models;
using GameStore.Models.Requests;
using GameStore.Utils;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace GameStore.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _collection;

        public async Task<bool> UserExists(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);

            var existingUser = await _collection.Find(filter).FirstOrDefaultAsync();
            return existingUser != null;
        }


        public UserService(IMongoDatabase database)
        {
            _collection = database.GetCollection<User>("Users");
        }


        public async Task<User> Register(User user)
        {
            if (await UserExists(user.Username))
            {
                return null;
            }

            user.CreateDate = DateTime.Now;

            await _collection.InsertOneAsync(user);

            return user;
        }


        public async Task<List<User>> GetAll()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }


        public async Task<User> GetById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<User> GetByUsername(string username)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Username, username);

            var existingUser = await _collection.Find(filter).FirstOrDefaultAsync();
            return existingUser;
        }


        public async Task<User> GetByUsernameOrEmail(string usernameOrEmail)
        {
            var filter = Builders<User>.Filter.Or(
                Builders<User>.Filter.Eq(u => u.Username, usernameOrEmail),
                Builders<User>.Filter.Eq(u => u.Email, usernameOrEmail)
            );

            return await _collection.Find(filter).FirstOrDefaultAsync();
        }


        public async Task<DeleteResult> DeleteAll()
        {
            return await _collection.DeleteManyAsync(_ => true);
        }


        public async Task<DeleteResult> DeleteById(string id)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, id);
            return await _collection.DeleteOneAsync(_ => true);

        }



        public async Task<Dictionary<string, object>> Update(UserRequest user)
        {
            var filter = Builders<User>.Filter.Eq(u => u.Id, user.Id);

            var updateDefinitions = new List<UpdateDefinition<User>>();
            var updateFields = new Dictionary<string, object>();

            if (user.Username != null)
            {
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Username, user.Username));
                updateFields.Add(AppUtils.FirstCharLow(nameof(User.Username)), user.Username);
            }

            if (user.Email != null)
            {
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Email, user.Email));
                updateFields.Add(AppUtils.FirstCharLow(nameof(User.Email)), user.Email);
            }

            if (user.Role != null)
            {
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Role, user.Role));
                updateFields.Add(AppUtils.FirstCharLow(nameof(User.Role)), user.Role);
            }

            if (user.Img != null)
            {
                updateDefinitions.Add(Builders<User>.Update.Set(p => p.Img, user.Img));
                updateFields.Add(AppUtils.FirstCharLow(nameof(User.Img)), user.Img);
            }


            var update = Builders<User>.Update.Combine(updateDefinitions);
            await _collection.UpdateOneAsync(filter, update);
            return updateFields;
        }
    }
}
