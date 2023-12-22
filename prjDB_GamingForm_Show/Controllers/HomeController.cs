//using AspNetCore;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MimeKit;
using Newtonsoft.Json;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Diagnostics;
using System.Net.Mail;
using System.Text.Json;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

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
        public void SendEmail()
        {
            int ValNumber = new Random().Next(1000, 10000);
            HttpContext.Session.SetInt32(CDictionary.SK_Validation_Number, ValNumber);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
            message.To.Add(new MailboxAddress("alan90306", "alan90306@gmail.com"));
            message.Subject = "Test mail of asp.net Core";
            message.Body = new TextPart("html")
            {
                Text = "<!DOCTYPE html>" +
                        "<html>" +
                        "<h2>您的驗證碼</h2>" +
                        "<p>您的驗證碼為:" + HttpContext.Session.GetInt32(CDictionary.SK_Validation_Number) + "</p>" +
                        "</html>"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
                client.Send(message);
                client.Disconnect(true);
            }
        }

        [HttpPost]
        public IActionResult ForgetPassword(CLoginViewModel CL)
        {
            string result = "沒有此信箱";
            var Emails = from e in _db.Members
                         select e.Email;
            if (Emails.Contains(CL.txtConfirm_Email))
            {
                SendEmail();
                result = "已寄出信件";
            }
            return Content(result);
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
        public IActionResult DeputeInfo() 
        {
            var datas = new
            {
                DeputeNumber = _db.Deputes.Count(),
                DeputeAvgPrice = _db.Deputes.Average(p=>p.Salary),
                HotRegion = _db.Regions.Include(R=>R.Deputes).OrderByDescending(D=>D.Deputes.Count()).Take(1)
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