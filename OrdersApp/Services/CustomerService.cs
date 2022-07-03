using Microsoft.EntityFrameworkCore;
using OrdersApp.Contracts;
using OrdersApp.Models;

namespace OrdersApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly OrdersAppContext db;

        public CustomerService(OrdersAppContext context)
        {
            db = context;
        }

        public void CreateCustomer(Customer customer)
        {
            db.Add(customer);
            db.SaveChanges();
        }

        public bool CustomerExists(int id)
        {
            return (db.Customers?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public void DeleteConfirmedCustomer(Customer customer)
        {
            db.Customers.Remove(customer);
            db.SaveChanges();
        }

        public Customer DeleteCustomer(int? id)
        {
            return db.Customers.Include(o => o.Orders).FirstOrDefault(m => m.Id == id);
        }

        public void UpdateCustomer(Customer customer)
        {
            db.Update(customer);
            db.SaveChanges();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return db.Customers.ToList();
        }

        public IEnumerable<Customer> GetAllCustomersIncludeOrders()
        {
            return db.Customers.Include(o => o.Orders).ToList();
        }

        public Customer GetCustomer(int? id)
        {
            return db.Customers.Find(id);
        }

        public bool DBCustomersIsNull()
        {
            return db.Customers == null;
        }
    }
}
