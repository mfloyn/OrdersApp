using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OrdersApp.Contracts;
using OrdersApp.Models;

namespace OrdersApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        // GET: Orders
        public IActionResult AllOrders()
        {
            return !_service.DBOrdersIsNull() ?
                View(_service.GetAllOrdersIncludeCustomers()) :
                Problem("Entity set 'ApplicationContext.Orders'  is null.");
        }
        

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null || _service.DBOrdersIsNull())
            {
                return NotFound();
            }

            var order = _service.DetailsOrder(id);
            return order == null ? NotFound() : View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewBag.Customers = new SelectList(_service.GetCustomersList(), "Id", "Surename");
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("OrderId,OrderName,OrderDate,Cost,CustomerId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _service.CreateOrder(order);
                return RedirectToAction(nameof(AllOrders));
            }
            ViewData["CustomerId"] = new SelectList(_service.GetCustomersList(), "Id", "Surename", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null || _service.DBOrdersIsNull())
            {
                return NotFound();
            }

            var order = _service.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["CustomerId"] = new SelectList(_service.GetCustomersList(), "Id", "Surename", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("OrderId,OrderName,OrderDate,Cost,CustomerId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _service.UbdateOrder(order);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_service.OrderExist(order.OrderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AllOrders));
            }
            ViewData["CustomerId"] = new SelectList(_service.GetCustomersList(), "Id", "Surename", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null || _service.DBOrdersIsNull())
            {
                return NotFound();
            }

            var order = _service.DetailsOrder(id);

            return order == null ? NotFound() : View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (_service.DBOrdersIsNull())
            {
                return Problem("Entity set 'ApplicationContext.Orders'  is null.");
            }

            var order = _service.GetOrder(id);
            if (order != null)
            {
                _service.DeleteConfirmedOrder(order);
            }

            return RedirectToAction(nameof(AllOrders));
        }      
    }
}
