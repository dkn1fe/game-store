using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace GameStore.Models.Dto
{
    public class ProductDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }

        public string Game { get; set; }

        public double Price { get; set; }

        public string Img { get; set; }

        public bool IsAviable { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
