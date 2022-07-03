using OrdersApp.Models;

namespace OrdersApp.Contracts
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        IEnumerable<Customer> GetAllCustomersIncludeOrders();
        Customer GetCustomer(int? id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        Customer DeleteCustomer(int? id);
        void DeleteConfirmedCustomer(Customer customer);
        bool CustomerExists(int id);
        bool DBCustomersIsNull();
    }
}
