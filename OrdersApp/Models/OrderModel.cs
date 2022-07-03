using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersApp.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Не указано название")]
        public string? OrderName { get; set; }

        [Required(ErrorMessage = "Не указана дата")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Не указана стоимость")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "Не выбран заказчик")]
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
