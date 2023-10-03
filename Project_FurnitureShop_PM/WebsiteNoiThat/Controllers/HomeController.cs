using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebsiteNoiThat.Models;

namespace WebsiteNoiThat.Controllers
{
    public class HomeController : Controller
    {

       
        public IActionResult Index()
        {
            return View();
        }

       
    }
}