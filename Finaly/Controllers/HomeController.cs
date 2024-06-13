
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Finaly.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

       
    }
}
