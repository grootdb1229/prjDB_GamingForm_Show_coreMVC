using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Diagnostics;

namespace prjDB_GamingForm_Show.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult HomePage()
        {
            return View();
        }
        public IActionResult DeputeMain()
        {
            return View();
        }

        public IActionResult ShoppingMain()
        {
            return View();
        }

        public IActionResult BlogMain()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public IActionResult Login(CLoginViewModel vm)
        //{
        //    DbGamingFormTestContext db = new DbGamingFormTestContext();
        //    Member user = (Member)(from m in db.Members
        //                           where m.Email == vm.txtAccount
        //                           select m);
        //    if (user != null) 
        //    {
        //        if (user.Password.Equals(vm.txtPassword)) 
        //        {
        //            HttpContext.Session[CDictionary.SK_Logged_User] =
        //        }
        //    }
             
        //}


        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
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