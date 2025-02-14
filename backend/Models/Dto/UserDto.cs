using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameStore.Models.Dto
{
    public class UserDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Time")]
        public DateTime CreateDate { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public string? Img { get; set; }
    }
}
