using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public AdminController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        int i每頁筆數 = 16;
        int i頁數 = 0;
        public IActionResult MemberList()
        {        
            var members = from m in _db.Members.Skip(i每頁筆數*i頁數).Take(i每頁筆數)
                          select m;           

            return View(members);
        }
        public IActionResult MemberListNext()
        {
            if (HttpContext.Session.Keys.Contains("number"))
            {
                i頁數 = (int)HttpContext.Session.GetInt32("number");
            }
            if (i頁數 < _db.Members.Count() / i每頁筆數)
            {
                i頁數++;
            }
            
            HttpContext.Session.SetInt32("number", i頁數);            
            var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                          select m;
            return View(members);
        }
        public IActionResult MemberListPrevious()
        {
            if (HttpContext.Session.Keys.Contains("number"))
            {
                i頁數 = (int)HttpContext.Session.GetInt32("number");
            }
            if (i頁數 != 0)
            {
                i頁數--;
            }

            HttpContext.Session.SetInt32("number", i頁數);
            var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                          select m;
            return View(members);
        }
    }
}
