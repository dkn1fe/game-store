using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Runtime.Serialization;

namespace GameStore.Models
{
    public class Product
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [DataMember]
        public string ProductId
        {
            get { return _id.ToString(); }
            set { _id = ObjectId.Parse(value); }
        }
        public string Title { get; set; }
        public string Game { get; set; }
        public double Price { get; set; }
        public string ImgSrc { get; set; }
    }
}
