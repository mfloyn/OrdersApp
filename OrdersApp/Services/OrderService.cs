using Microsoft.EntityFrameworkCore;
using OrdersApp.Contracts;
using OrdersApp.Models;

namespace OrdersApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly OrdersAppContext db;

        public OrderService(OrdersAppContext context)
        {
            db = context;
        }

        public Order GetOrder(int? id)
        {
            return db.Orders.Find(id);
        }
        public IEnumerable<Order> GetAllOrdersIncludeCustomers()
        {
            return db.Orders.Include(o => o.Customer).ToList();
        }

        public void CreateOrder(Order order)
        {
            db.Add(order);
            db.SaveChanges();
        }

        public Order DetailsOrder(int? id)
        {
            return db.Orders.Include(o => o.Customer).FirstOrDefault(m => m.OrderId == id);
        }

        public void UbdateOrder(Order order)
        {
            db.Update(order);
            db.SaveChanges();
        }

        public void DeleteConfirmedOrder(Order order)
        {
            db.Orders.Remove(order);
            db.SaveChanges();
        }

        public IEnumerable<Customer> GetCustomersList()
        {
            return db.Customers.ToList();
        }

        public bool OrderExist(int id)
        {
            return (db.Orders?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }

        public bool DBOrdersIsNull()
        {
            return db.Orders == null;
        }
    }
}
