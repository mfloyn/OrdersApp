using System.ComponentModel.DataAnnotations;

namespace OrdersApp.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        public string? Surename { get; set; }

        [RegularExpression(@"(\+7|8|\b)[\(\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[)\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)[\s-]*(\d)", ErrorMessage = "Некорректный номер телефона")]
        [Required(ErrorMessage = "Укажите номер")]
        public string? PhoneNumber { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Некорректный адрес почты")]
        [Required(ErrorMessage = "Укажите Email")]
        public string? Email { get; set; }

        public List<Order> Orders { get; set; } = new();

    }
}
