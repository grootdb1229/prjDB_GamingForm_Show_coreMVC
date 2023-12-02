using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;

namespace prjDB_GamingForm_Show.Controllers
{
    public class AdminController : AdminUseSuperController
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
            string name = TempData["AdminName"] as string;
            ViewBag.Name = name;
            return View();
        }
        int i每頁筆數 = 8;
        int i頁數 = 0;
        public IActionResult MemberList(CKeyWordViewModel vm)
        {
            HttpContext.Session.Remove(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字);
            if (string.IsNullOrEmpty(vm.txtKeyWord))
            {
                var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                              select m;

                return View(members);
            }
            else
            {
                var members = from m in _db.Members.Where(m => m.Email.Contains(vm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                              select m;
                return View(members);
            }
        }
        public IActionResult MemberListNext(CKeyWordViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.txtKeyWord))
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
            else
            {
                var members = from m in _db.Members.Where(m => m.Email.Contains(vm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                              select m;
                return View(members);
            }

        }
        public IActionResult MemberListPrevious(CKeyWordViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.txtKeyWord))
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
            else
            {
                var members = from m in _db.Members.Where(m => m.Email.Contains(vm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                              select m;
                return View(members);
            }
        }
        public IActionResult BlogCategoryList(CKeyWordViewModel kyvm)
        {
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                var bc = from b in _db.SubTags.Where(tid => tid.TagId == 4)
                         select b;
                return View(bc);
            }
            else
            {
                var bc = from b in _db.SubTags.Where(tid => tid.TagId == 4 && tid.Name.Contains(kyvm.txtKeyWord))
                         select b;
                return View(bc);
            }
        }
        public IActionResult BlogList(CKeyWordViewModel kyvm)
        {
            HttpContext.Session.Remove(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
        }
        public IActionResult BlogListNext(CKeyWordViewModel kyvm)
        {
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字))
                {
                    i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
                }
                if (i頁數 < _db.Blogs.Count() / i每頁筆數)
                {
                    i頁數++;
                }
                HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字, i頁數);
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
        }
        public IActionResult BlogListPrevious(CKeyWordViewModel kyvm)
        {
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字))
                {
                    i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
                }
                if (i頁數 != 0)
                {
                    i頁數--;
                }
                HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字, i頁數);
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
        }
        public IActionResult ProductList(CKeyWordViewModel kyvm)
        {
            HttpContext.Session.Remove(CDictionary.SK_管理者觀看商品清單頁數使用關鍵字);
            CAdminCheckProductViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };

                return View(vm);
            }
            else
            {
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Where(p => p.ProductName.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }

        }
        public IActionResult ProductListNext(CKeyWordViewModel kyvm)
        {
            CAdminCheckProductViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
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
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }
            else
            {
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Where(p => p.ProductName.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }
        }
        public IActionResult ProductListPrevious(CKeyWordViewModel kyvm)
        {
            CAdminCheckProductViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
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
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }
            else
            {
                vm = new CAdminCheckProductViewModel
                {
                    Products = _db.Products.Include(m => m.Member).Where(p => p.ProductName.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    Members = _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }
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
                if (product.StatusId == 1)
                {
                    product.StatusId = 2;
                }
                else if (product.StatusId == 2)
                {
                    product.StatusId = 1;
                }
                _db.SaveChanges();
            }
            return RedirectToAction("ProductList");
        }
        public IActionResult SignalRTest()
        {
            return View();
        }
        public IActionResult ProductReview()
        {
            CAdminCheckProductViewModel vm = new CAdminCheckProductViewModel
            {
                Products = _db.Products.Include(m => m.Member).Where(p => p.StatusId == 4),
                Members = _db.Members
            };            
            return View(vm);
        }
        public IActionResult SignalRPV()
        {
            List<CSignalRUseAdminList> Admins = new List<CSignalRUseAdminList>();
            var datas = from a in _db.Admins
                        select new
                        {
                            a.AdminId,
                            a.Name
                        };
            foreach (var data in datas)
            {
                CSignalRUseAdminList Admin = new CSignalRUseAdminList();
                Admin.AdminId = data.AdminId;
                Admin.AdminName = data.Name;
                Admins.Add(Admin);
            }
                
            return PartialView(Admins);
        }
        //public IActionResult MemberListNexttest()
        //{
        //    if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字))
        //    {
        //        i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字);
        //    }
        //    if (i頁數 < _db.Members.Count() / i每頁筆數)
        //    {
        //        i頁數++;
        //    }

        //    HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看會員清單頁數使用關鍵字, i頁數);
        //    var members = from m in _db.Members.Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
        //                  select m;
        //    return Json(members);
        //}
    }
}
