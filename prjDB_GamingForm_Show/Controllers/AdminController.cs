using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;

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
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字))
            {
                i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字);
            }
            if (i頁數 < _db.Members.Count() / i每頁筆數)
            {
                i頁數++;
            }
            
            HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字, i頁數);            
            var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                          select m;
            return View(members);
        }
        public IActionResult MemberListPrevious()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字))
            {
                i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字);
            }
            if (i頁數 != 0)
            {
                i頁數--;
            }

            HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字, i頁數);
            var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                          select m;
            return View(members);
        }
        public IActionResult BlogCategoryList()
        {
            var bc = from b in _db.SubTags.Where(tid => tid.TagId == 4)
                     select b;
            return View(bc);
        }
        public IActionResult BlogList()
        {
            CBlogViewModel vm = new CBlogViewModel
            {
                blogs = _db.Blogs.Include(p=>p.SubTag),
                subTags = _db.SubTags
            };
            
            return View(vm);
        }
        public IActionResult ProductList()
        {
            CAdminCheckProductViewModel vm = new CAdminCheckProductViewModel
            {
                Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
            };
            
            return View(vm);
        }
        public IActionResult ProductListNext()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字))
            {
                i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字);
            }
            if (i頁數 < _db.Products.Count() / i每頁筆數)
            {
                i頁數++;
            }

            HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字, i頁數);
            CAdminCheckProductViewModel vm = new CAdminCheckProductViewModel
            {
                Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
            };
            return View(vm);
        }
        public IActionResult ProductListPrevious()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字))
            {
                i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字);
            }
            if (i頁數 != 0)
            {
                i頁數--;
            }
            HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字, i頁數);
            CAdminCheckProductViewModel vm = new CAdminCheckProductViewModel
            {
                Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
            };
            return View(vm);
        }
        public IActionResult ProductStatusEdit(int id)
        {
            Product product = _db.Products.FirstOrDefault(p => p.ProductId == id);
            if (product == null)
            {
                return RedirectToAction("ProductList");
            }
            else
            {
                if(product.StatusId == 1)
                {
                    product.StatusId = 2;
                }
                else if(product.StatusId == 2)
                {
                    product.StatusId = 1;
                }
                _db.SaveChanges();
            }
            return RedirectToAction("ProductList");
        }
    }
}
