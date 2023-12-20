//using AspNetCore;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
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
        public IActionResult test()
        {
            return View();
        }
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
            {
                HttpContext.Session.Remove(CDictionary.SK_UserID);
                HttpContext.Session.Remove(CDictionary.SK_UserName);
                HttpContext.Session.Remove(CDictionary.SK_PURCHASED_PRODUCES_LIST);
            }
            return RedirectToAction("HomePage");
        }
        public IActionResult ShopInfo()
        {
            var datas = new
            {
                proCount = _db.Products.Count(),//商品數
                orderCount = _db.Orders.Count(),//交易數
                orderProductCount = _db.OrderProducts.Count(),//銷售量
            };
            return Json(datas);
        }
        public IActionResult BlogInfo()
        {
            _db.SubBlogs.Load();
            _db.Articles.Load();
            var datas = new
            {

                ArticleCounts = _db.Articles.Count(),
                MPB = _db.Blogs.OrderByDescending(b => b.SubBlogs.SelectMany(s => s.Articles).Sum(a => a.ViewCount)).Select(a => a.Title).Take(1),
                MAM = _db.Members.OrderByDescending(m=>m.Articles.Count()).Take(1),

            };
            return Json(datas);
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
                    string user_name = user.Name;
                    HttpContext.Session.SetInt32(CDictionary.SK_UserID, user_id);
                    HttpContext.Session.SetString(CDictionary.SK_UserName, user_name);
                    ViewBag.user_id = user_id;
                    ViewBag.user_name = user_name;
                    //string user_Serialized = System.Text.Json.JsonSerializer.Serialize(user);
                    //HttpContext.Session.SetString(CDictionary.SK_Logged_User, user_Serialized);
                    //ViewBag.LoggedUser = user_Serialized;

                    //1123test
                    string returnUrl = Request.Cookies["returnUrl"];
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("HomePage", "Home");
                    }
                    else
                    {
                        
                        Response.Cookies.Delete("returnUrl");
                        return Redirect(returnUrl);
                    }
                }
                return RedirectToAction("HomePage", "Home");

            }
            return RedirectToAction("Create", "Member");
        }
        public IActionResult PopularShopItems() 
        {
            var datas = from P in _db.Products
                       orderby P.ViewCount descending 
                       select new { P.ProductName, P.AvailableDate, P.ProductContent , P.FImagePath , P.ProductId };
            return Json(datas);
        }

        public IActionResult PopularArticles() 
        {
            var datas = _db.Articles.Include(a => a.SubBlog).ThenInclude(b => b.Blog)
                        .OrderBy(a => a.ViewCount).Take(6).
                        Select(a => a);
            return Json(datas);          
        }

        public IActionResult PopularDeputes()
        {
            var datas = from D in _db.Deputes
                        orderby D.ViewCount descending
                        select new { D.Title, D.DeputeContent , D.DeputeId };

            return Json(datas);
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