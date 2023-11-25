using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Diagnostics;
using System.Text.Json;

namespace prjDB_GamingForm_Show.Controllers
{
    public class HomeController : Controller
    {

        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public HomeController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }

        private readonly ILogger<HomeController> _logger;

        public IActionResult HomePage()
        {
            ViewBag.LoggedUser = CDictionary.SK_Logged_User;
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

        [HttpPost]
        public IActionResult Login(CLoginViewModel vm)
        {
            Member user = _db.Members.FirstOrDefault(m => m.Email.Equals(vm.txtAccount));
            if (user != null)
            {
                if (user.Password.Equals(vm.txtPassword))
                {
                    int user_id = user.MemberId;
                    HttpContext.Session.SetInt32("user_id", user_id);
                    ViewBag.user_id = user_id;
                    //string user_Serialized = System.Text.Json.JsonSerializer.Serialize(user);
                    //HttpContext.Session.SetString(CDictionary.SK_Logged_User, user_Serialized);
                    //ViewBag.LoggedUser = user_Serialized;

                    //1123test
                    string returnUrl = HttpContext.Session.GetString("returnUrl");
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                    else
                    {
                        HttpContext.Session.Remove("returnUrl");
                        return Redirect(returnUrl);
                    }
                }

            }
            return RedirectToAction("Create", "Member");
        }


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

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