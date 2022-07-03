using Microsoft.EntityFrameworkCore;

namespace OrdersApp.Models
{
    public class OrdersAppContext : DbContext
    {
        public OrdersAppContext(DbContextOptions<OrdersAppContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        //первичная инициализация БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Customer customer1= new()
            {
                Id=1,
                Name="Андрей",
                Surename= "Дубровин",
                PhoneNumber="89378854615",
                Email="dubrrovin@mail.ru"
            };
            Customer customer2 = new()
            {
                Id = 2,
                Name = "Иван",
                Surename = "Скачков",
                PhoneNumber = "89215467895",
                Email = "scachkov@yandex.ru"
            };
            Customer customer3 = new()
            {
                Id = 3,
                Name = "Руслан",
                Surename = "Абрамов",
                PhoneNumber = "89514564574",
                Email = "abramovv@gmail.com"
            };
            Customer customer4 = new()
            {
                Id = 4,
                Name = "Дмитрий",
                Surename = "Царев",
                PhoneNumber = "89514564574",
                Email = "tsarevdmi@gmail.com"
            };


            Order order1 = new() 
            { 
                OrderId=1, 
                OrderName="Разработка сайта",
                OrderDate=DateTime.Now.AddDays(-1),
                Cost=35000,
                CustomerId=customer3.Id            
            };
            Order order2 = new()
            {
                OrderId = 2,
                OrderName = "Адаптивная верстка",
                OrderDate = DateTime.Now.AddDays(-2),
                Cost = 25000,
                CustomerId = customer3.Id
            };
            Order order3 = new()
            {
                OrderId = 3,
                OrderName = "Разработка макета",
                OrderDate = DateTime.Now.AddDays(-6),
                Cost = 15000,
                CustomerId = customer2.Id
            };
            Order order4 = new()
            {
                OrderId = 4,
                OrderName = "Разработка MVC",
                OrderDate = DateTime.Now.AddDays(-3),
                Cost = 110000,
                CustomerId = customer1.Id
            };

            modelBuilder.Entity<Customer>().HasData(customer1, customer2, customer3, customer4);
            modelBuilder.Entity<Order>().HasData(order1, order2, order3, order4);
        }

        public DbSet<Customer>? Customers { get; set; }
        public DbSet<Order>? Orders { get; set; }
    }
}
