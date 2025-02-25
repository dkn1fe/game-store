using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using GameStore.Models.Dto;

namespace GameStore.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Название обязательно.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Игра обязательна.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов.")]
        public string Game { get; set; }

        [Required(ErrorMessage = "Цена обязательна.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Изображние обязательно.")]
        [Url(ErrorMessage = "Некорректный URL изображения.")]
        public string Img { get; set; }

        public string? ImgId { get; set; }

        public bool IsAviable { get; set; }

        public DateTime CreationDate { get; set; }

        public ProductDto ToProductDto()
        {
            return new ProductDto
            {
                Id = Id,
                Title = Title,
                Game = Game,
                Price = Price,
                Img = Img,
                IsAviable = IsAviable,
                CreationDate = CreationDate,
            };
        }
    }
}
