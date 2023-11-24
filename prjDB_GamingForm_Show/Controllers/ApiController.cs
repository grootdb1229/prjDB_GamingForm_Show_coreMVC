using Microsoft.AspNetCore.Mvc;

namespace prjDB_GamingForm_Show.Controllers
{
    public class ApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Vision()
        {
            return View();
        }

        public IActionResult VisionBinary()
        {
            return View();
        }
    }
}
