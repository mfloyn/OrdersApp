using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrdersApp.Contracts;
using OrdersApp.Models;
using System.Diagnostics;


namespace OrdersApp.Controllers
{
    public class HomeController : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly IOrderService _service;

        public HomeController(IOrderService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return !_service.DBOrdersIsNull() ?
                View(_service.GetAllOrdersIncludeCustomers()) :
                Problem("Entity set 'ApplicationContext.Orders' is null.");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}