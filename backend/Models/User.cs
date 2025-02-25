using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using GameStore.Models.Dto;

namespace GameStore.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [Required(ErrorMessage = "Имя пользователя обязательно.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 100 символов.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль обязателен.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email обязателен.")]
        [EmailAddress(ErrorMessage = "Неверный формат email.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Роль обязательна.")]
        public string Role { get; set; }

        public DateTime CreationDate { get; set; }

        public string? Img { get; set; }

        public string? ImgId { get; set; }

        public UserDto ToUserDto()
        {
            return new UserDto
            {
                Id = Id,
                Username = Username,
                Email = Email,
                CreationDate = CreationDate,
                Role = Role,
                Img = Img,
            };
        }
    }
}
