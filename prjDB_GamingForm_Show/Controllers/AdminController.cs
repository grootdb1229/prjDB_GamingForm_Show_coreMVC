using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Security.Cryptography;

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
        public IActionResult ChatPV(int id)
        {
            var senderid = _db.Admins.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_管理者名稱)).Select(a => a.AdminId).FirstOrDefault();
            List<CAdminChatViewModel> chats = new List<CAdminChatViewModel>();
            ViewBag.SendToName = _db.Admins.Where(a => a.AdminId == id).Select(a => a.Name).FirstOrDefault();
            foreach (var chat in _db.Chats)
            {
                if (chat.SenderAdmin == senderid && chat.ReceiveAdmin == id)
                {
                    CAdminChatViewModel ori = new CAdminChatViewModel()
                    {
                        SenderName = HttpContext.Session.GetString(CDictionary.SK_管理者名稱),
                        ReceiverName = _db.Admins.Where(a => a.AdminId == id).Select(a => a.Name).FirstOrDefault(),
                        SenderId = senderid,
                        ReceiverId = id,
                        SenderImg = _db.Admins.Where(a => a.AdminId == senderid).Select(a => a.ImgPath).FirstOrDefault(),
                        ReceiverImg = _db.Admins.Where(a => a.AdminId == id).Select(a => a.ImgPath).FirstOrDefault(),
                        ChatContent = chat.ChatContent,
                        ModefiedDate = chat.ModefiedDate,
                        IsCheck = chat.IsCheck
                    };
                    chats.Add(ori);
                }
                else if (chat.SenderAdmin == id && chat.ReceiveAdmin == senderid)
                {
                    CAdminChatViewModel ori = new CAdminChatViewModel()
                    {
                        SenderName = _db.Admins.Where(a => a.AdminId == id).Select(a => a.Name).FirstOrDefault(),
                        ReceiverName = HttpContext.Session.GetString(CDictionary.SK_管理者名稱),
                        SenderId = id,
                        ReceiverId = senderid,
                        SenderImg = _db.Admins.Where(a => a.AdminId == senderid).Select(a => a.ImgPath).FirstOrDefault(),
                        ReceiverImg = _db.Admins.Where(a => a.AdminId == id).Select(a => a.ImgPath).FirstOrDefault(),
                        ChatContent = chat.ChatContent,
                        ModefiedDate = chat.ModefiedDate,
                        IsCheck = chat.IsCheck
                    };
                    chats.Add(ori);
                }
            }
            if (chats != null && chats.Count > 0)
            {
                return PartialView(chats);
            }
            else
            {
                CAdminChatViewModel newchat = new CAdminChatViewModel()
                {
                    SenderName = HttpContext.Session.GetString(CDictionary.SK_管理者名稱),
                    ReceiverName = _db.Admins.Where(a => a.AdminId == id).Select(a => a.Name).FirstOrDefault(),
                    SenderId = senderid,
                    ReceiverId = id,
                    SenderImg = _db.Admins.Where(a => a.AdminId == senderid).Select(a => a.ImgPath).FirstOrDefault(),
                    ReceiverImg = _db.Admins.Where(a => a.AdminId == id).Select(a => a.ImgPath).FirstOrDefault(),
                };
                chats.Add(newchat);
                return PartialView(chats);
            }
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

        //---------------------------論壇---------------------------
        #region 論壇

        public IActionResult BlogCategoryList(CKeyWordViewModel kyvm)
        {
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                var bc = from b in _db.SubTags.Include(p => p.Tag).Where(tid => tid.TagId == 4)
                         select b;
                return View(bc);
            }
            else
            {
                var bc = from b in _db.SubTags.Include(p => p.Tag).Where(tid => tid.TagId == 4 && tid.Name.Contains(kyvm.txtKeyWord))
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
                    subTags = _db.SubTags.Include(p=>p.Tag)
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)||b.SubTag.Name.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags.Include(p => p.Tag)
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


        public IActionResult BlogSubTagEdit(int? SId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                subTags = _db.SubTags.Where(P=>P.SubTagId == SId).Select(a=>a)
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult BlogSubTagEdit(SubTag Snart, int? SId )
        {
            SubTag dbsub = _db.SubTags.First(a => a.SubTagId == Snart.SubTagId);
            if (dbsub != null)
            { 
            dbsub.Name = Snart.Name;
            }
            _db.SaveChanges();
            return RedirectToAction("BlogCategoryList");
        }

        
        public ActionResult BlogSubTagDelete(int? SId)
        {
            SubTag dbsub = _db.SubTags.FirstOrDefault(a => a.SubTagId == SId);
            if (dbsub != null)
            {
                dbsub.TagId = 5;
                _db.SaveChanges();
            }
            return RedirectToAction("BlogCategoryList");

        }



        //--1207--未完工
        public IActionResult BlogEdit(int? BId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                subTags = _db.SubTags.Where(P => P.SubTagId == SId).Select(a => a),
                

            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult BlogEdit(SubTag Snart, int? SId)
        {
            SubTag dbsub = _db.SubTags.First(a => a.SubTagId == Snart.SubTagId);
            if (dbsub != null)
            {
                dbsub.Name = Snart.Name;
            }
            _db.SaveChanges();
            return RedirectToAction("BlogCategoryList");
        }



        #endregion
        //---------------------------論壇---------------------------
    }
    }
