using Microsoft.AspNetCore.Mvc;
using Project_FurnitureShop_PM.Models;
using System.Diagnostics;

namespace Project_FurnitureShop_PM.Controllers
{
    public class HomeController : Controller
    {
        

        public HomeController()
        {
           
        }

        public IActionResult Index()
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