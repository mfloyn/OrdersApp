using OrdersApp.Models;

namespace OrdersApp.Contracts
{
    public interface IOrderService
    {
        Order GetOrder(int? id);
        Order DetailsOrder(int? id);
        IEnumerable<Customer> GetCustomersList();
        IEnumerable<Order> GetAllOrdersIncludeCustomers();
        void CreateOrder(Order order);
        void UbdateOrder(Order order);
        void DeleteConfirmedOrder(Order order);
        bool OrderExist(int id);
        bool DBOrdersIsNull();
    }
}
