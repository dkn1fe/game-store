using GameStore.Models.Dto;
using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Requests
{
    public class UserRequest
    {
        public string Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 100 символов.")]
        public string? Username { get; set; }

        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен быть не менее 6 символов.")]
        public string? Password { get; set; }

        [EmailAddress(ErrorMessage = "Неверный формат email.")]
        public string? Email { get; set; }

        public string? Role { get; set; }

        [Url(ErrorMessage = "Некорректный URL изображения.")]
        public string? Img { get; set; }

        public UserDto ToUserDto()
        {
            return new UserDto
            {
                Id = Id,
                Username = Username,
                Email = Email,
                Role = Role,
                Img = Img,
            };
        }
    }
}
