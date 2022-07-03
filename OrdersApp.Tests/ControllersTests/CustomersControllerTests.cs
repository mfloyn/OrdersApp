using OrdersApp.Contracts;
using OrdersApp.Controllers;
using OrdersApp.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace OrdersApp.Tests.ControllersTests
{
    public class CustomersControllerTests
    {
        private readonly Mock<ICustomerService> _customersServiceMock;
        private readonly CustomersController _customersController;

        public CustomersControllerTests()
        {
            _customersServiceMock = new Mock<ICustomerService>();
            _customersController = new CustomersController(_customersServiceMock.Object);
        }

        [Fact]
        public void AllCustomers_ActionExecutes_ReturnsView()
        {
            var result = _customersController.AllCustomers();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AllCustomers_ActionExecutes_ReturnsTheNumberOfCustomers()
        {
            _customersServiceMock.Setup(service => service.GetAllCustomersIncludeOrders())
                .Returns(new List<Customer>() { new Customer(), new Customer() });

            var result = _customersController.AllCustomers();

            var viewResult = Assert.IsType<ViewResult>(result);
            var Customers = Assert.IsType<List<Customer>>(viewResult.Model);
            Assert.Equal(2, Customers.Count);
        }

        [Fact]
        public void Create_InvalidModelState_ReturnsView()
        {
            _customersController.ModelState.AddModelError("Name", "Name is required");
            _customersController.ModelState.AddModelError("Surename", "Surename is required");
            _customersController.ModelState.AddModelError("PhoneNumber", "PhoneNumber is required");
            _customersController.ModelState.AddModelError("Email", "Email is required");


            var customer = new Customer { 
                Name = "Имя", 
                Surename = "Фам", 
                PhoneNumber="89000000000", 
                Email="mail@mail.ru"};

            var result = _customersController.Create(customer);

            var viewResult = Assert.IsType<ViewResult>(result);
            var testCustomer = Assert.IsType<Customer>(viewResult.Model);

            Assert.Equal(customer.Name, testCustomer.Name);
            Assert.Equal(customer.Surename, testCustomer.Surename);
            Assert.Equal(customer.PhoneNumber, testCustomer.PhoneNumber);
            Assert.Equal(customer.Email, testCustomer.Email);
        }

        [Fact]
        public void Create_InvalidModelState_CreateCustomerNeverExecutes()
        {
            _customersController.ModelState.AddModelError("Name", "Name is required");
            _customersController.ModelState.AddModelError("Surename", "Surename is required");
            _customersController.ModelState.AddModelError("PhoneNumber", "PhoneNumber is required");
            _customersController.ModelState.AddModelError("Email", "Email is required");

            var Customer = new Customer 
            {
                Name = "Имя",
            };

            _customersController.Create(Customer);

            _customersServiceMock.Verify(x => x.CreateCustomer(It.IsAny<Customer>()), Times.Never);
        }

        [Fact]
        public void Create_ModelStateValid_CreateCustomerCalledOnce()
        {
            Customer? emp = null;

            _customersServiceMock.Setup(r => r.CreateCustomer(It.IsAny<Customer>()))
                .Callback<Customer>(x => emp = x);

            var Customer = new Customer
            {
                Name = "Имя",
                Surename = "Фам",
                PhoneNumber = "89000000000",
                Email = "mail@mail.ru"
            };

            _customersController.Create(Customer);
            _customersServiceMock.Verify(x => x.CreateCustomer(It.IsAny<Customer>()), Times.Once);

            Assert.Equal(emp.Name, Customer.Name);
            Assert.Equal(emp.Surename, Customer.Surename);
            Assert.Equal(emp.PhoneNumber, Customer.PhoneNumber);
            Assert.Equal(emp.Email, Customer.Email);
        }

        [Fact]
        public void Create_ActionExecuted_RedirectsToAllCustomers()
        {
            var Customer = new Customer
            {
                Name = "Имя",
                Surename = "Фам",
                PhoneNumber = "89000000000",
                Email = "mail@mail.ru"
            };

            var result = _customersController.Create(Customer);

            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Equal("AllCustomers", redirectToActionResult.ActionName);
        }
    }


}
