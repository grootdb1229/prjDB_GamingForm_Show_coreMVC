using Microsoft.AspNetCore.Mvc;

namespace prjDB_GamingForm_Show.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
