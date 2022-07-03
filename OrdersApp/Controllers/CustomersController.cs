using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApp.Contracts;
using OrdersApp.Models;

namespace OrdersApp.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ICustomerService _service;

        public CustomersController(ICustomerService service)
        {
            _service = service;
        }

        // GET: Customers
        public IActionResult AllCustomers()
        {
            var data = _service.GetAllCustomersIncludeOrders();
            return data != null ?
                View(_service.GetAllCustomersIncludeOrders()) :
                Problem("Entity set 'ApplicationContext.Customers'  is null.");
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Surename,PhoneNumber,Email")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _service.CreateCustomer(customer);
                return RedirectToAction(nameof(AllCustomers));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _service.DBCustomersIsNull())
            {
                return NotFound();
            }
            else
            {
                var customer = _service.GetCustomer(id);
                return customer == null ? NotFound() : View(customer);
            }
            
        }

        // POST: Customers/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Name,Surename,PhoneNumber,Email")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateCustomer(customer);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_service.CustomerExists(customer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllCustomers));
            }
            return View(customer);
        }

        // GET: Customers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _service.DBCustomersIsNull())
            {
                return NotFound();
            }

            var customer = _service.DeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_service.DBCustomersIsNull())
            {
                return Problem("Entity set 'ApplicationContext.Customers'  is null.");
            }
            var customer = _service.GetCustomer(id);
            if (customer != null)
            {
                _service.DeleteConfirmedCustomer(customer);
            }
            
            return RedirectToAction(nameof(AllCustomers));
        }
    }
}
