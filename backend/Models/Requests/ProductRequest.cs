using System.ComponentModel.DataAnnotations;

namespace GameStore.Models.Request
{
    public class ProductRequest
    {
        public string Id { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов.")]
        public string? Title { get; set; }

        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно быть от 3 до 50 символов.")]
        public string? Game { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Цена должна быть больше 0.")]
        public double? Price { get; set; }

        [Url(ErrorMessage = "Некорректный URL изображения.")]
        public string? Img { get; set; }

        public bool? IsAviable { get; set; }
    }
}
