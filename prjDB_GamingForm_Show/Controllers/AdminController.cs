using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Admin;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Drawing;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;

namespace prjDB_GamingForm_Show.Controllers
{
    public class AdminController : AdminUseSuperController
    {
        private IWebHostEnvironment _enviro = null; //論壇圖片
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public AdminController(IWebHostEnvironment host, DbGamingFormTestContext db, IWebHostEnvironment p)
        {
            _host = host;
            _db = db;
            _enviro = p;//論壇圖片
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
        public IActionResult ProductReview()
        {
            CAdminCheckProductViewModel vm = new CAdminCheckProductViewModel
            {
                Products = _db.Products.Include(m => m.Member).Where(p => p.StatusId == 7),
                Members = _db.Members
            };
            return View(vm);
        }
        public IActionResult ProductEdit(CProductAdmin vm)
        {
            var data = _db.ProductComplains.Where(n => n.Id == vm.txtID);

            foreach (var item in data)
            {
                item.StatusId = vm.txtStatusID;
            }
            _db.SaveChanges();
            return RedirectToAction("ProductComplain");
        }
        public IActionResult ProductComplain()
        { 
            List<CProductComplainViewModel> ProductComplain = new List<CProductComplainViewModel>();
            var datas = _db.ProductComplains.OrderBy(x=>x.Id).Select(x =>new {x.Id,x.ProductId,x.MemeberId,x.ReplyContent,x.ReportDate ,x.Status.Name} );
            CProductComplainViewModel pc = new CProductComplainViewModel();
            foreach (var data in datas)
            {
               pc.Id = data.Id;
               pc.ProductId= data.ProductId;
               pc.MemeberId= data.MemeberId;
               pc.ReplyContent= data.ReplyContent;
               //pc.ReportDate= data.ReportDate;
               pc.Status = data.Name;
               ProductComplain.Add(pc);
            }
            return View(ProductComplain);
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
        [HttpPost]
        public int HowMuchMessageNotCheck()
        {
            int count = 0;
            int reid = _db.Admins.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_管理者名稱)).Select(a => a.AdminId).FirstOrDefault();
            foreach (var m in _db.Chats)
            {
                if (m.ReceiveAdmin == reid && m.IsCheck == false)
                {
                    count++;
                }
            }
            return count;
        }
        
        public IActionResult NotCheckMessage()
        {
            int reid = _db.Admins.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_管理者名稱)).Select(a => a.AdminId).FirstOrDefault();
            List<CAdminNotCheckMessageViewModel> vm = new List<CAdminNotCheckMessageViewModel>();
            CAdminNotCheckMessageViewModel message = null;
            foreach(var m in _db.Chats)
            {
                if (m.ReceiveAdmin == reid && m.IsCheck == false)
                {
                    message = new CAdminNotCheckMessageViewModel()
                    {
                        senderName = _db.Admins.Where(a => a.AdminId == m.SenderAdmin).Select(a => a.Name).FirstOrDefault(),
                        senderImgPath = _db.Admins.Where(a => a.AdminId == m.SenderAdmin).Select(a => a.ImgPath).FirstOrDefault(),
                        message = m.ChatContent,
                        sendTime = MessageTime(m.ModefiedDate)
                    };
                    vm.Add(message);
                }
            }
            return Json(vm);
        }
        [HttpPost]
        public void CheckAllMessage(int senderid)
        {
            int reid = _db.Admins.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_管理者名稱)).Select(a => a.AdminId).FirstOrDefault();
            foreach (var m in _db.Chats)
            {
                if (m.ReceiveAdmin == reid && m.SenderAdmin == senderid && m.IsCheck == false)
                {
                    m.IsCheck = true;
                }
            }
            _db.SaveChanges();
        }

        public IActionResult CouponList(CKeyWordViewModel kyvm)
        {
            List<CAdminCouponViewModel> viewModel = new List<CAdminCouponViewModel>();
            CAdminCouponViewModel c = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                foreach (var m in _db.Coupons)
                {
                    c = new CAdminCouponViewModel()
                    {
                        CouponId = m.CouponId,
                        Title = m.Title,
                        Content = m.CouponContent,
                        Discount = m.Discount,
                        Reduce = m.Reduce,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        Type = m.StatusId.ToString(),
                    };
                    viewModel.Add(c);
                }
                return View(viewModel);
            }
            else
            {
                foreach (var m in _db.Coupons.Where(c => c.Title.Contains(kyvm.txtKeyWord)||c.CouponContent.Contains(kyvm.txtKeyWord)))
                {
                    c = new CAdminCouponViewModel()
                    {
                        CouponId = m.CouponId,
                        Title = m.Title,
                        Content = m.CouponContent,
                        Discount = m.Discount,
                        Reduce = m.Reduce,
                        StartDate = m.StartDate,
                        EndDate = m.EndDate,
                        Type = m.StatusId.ToString(),
                    };
                    viewModel.Add(c);
                }
                return View(viewModel);
            }
        }
        [HttpPost]
        public IActionResult CouponTypeEdit(int id)
        {
            Coupon coupon = _db.Coupons.FirstOrDefault(c => c.CouponId == id);
            if (coupon == null)
            {
                return RedirectToAction("CouponList");
            }
            else
            {
                if (coupon.StatusId == 23)
                {
                    coupon.StatusId = 24;
                }
                else if (coupon.StatusId == 24)
                {
                    coupon.StatusId = 23;
                }
                _db.SaveChanges();
            }
            return RedirectToAction("CouponList");
        }

        public IActionResult CouponCreat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CouponCreat(CAdminCouponViewModel vm)
        {
            Coupon coupon = new Coupon()
            {
                Title = vm.Title,
                CouponContent = vm.Content,
                Discount = vm.Discount,                
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                StatusId = 24
            };

            _db.Coupons.Add(coupon);
            _db.SaveChanges();
            return RedirectToAction("CouponList");
        }

        public IActionResult ReduceCreat()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ReduceCreat(CAdminCouponViewModel vm)
        {
            Coupon coupon = new Coupon()
            {
                Title = vm.Title,
                CouponContent = vm.Content,                
                Reduce = vm.Reduce,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate,
                StatusId = 24
            };

            _db.Coupons.Add(coupon);
            _db.SaveChanges();
            return RedirectToAction("CouponList");
        }
        public IActionResult fuck()
        {
            return View();
        }
        public string MessageTime(string time)
        {
            DateTime messagetime = DateTime.Parse(time);            
            DateTime now = DateTime.Now.ToLocalTime();
            TimeSpan timeSpan = now - messagetime;
            if(timeSpan.TotalDays >= 1)
            {
                return timeSpan.Days.ToString() + "天前";
            }
            else if (timeSpan.TotalHours >= 1)
            {
                return timeSpan.Hours.ToString() + "小時前";
            }
            else if (timeSpan.TotalMinutes >= 1)
            {
                return timeSpan.Minutes.ToString() + "分鐘前";
            }
            else
            {
                return "現在";
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
                    subTags = _db.SubTags.Include(p => p.Tag)
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubTag.Name.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags.Include(p => p.Tag)
                };
                return View(vm);
            }
        }

        public IActionResult BlogSubBlogList(CKeyWordViewModel kyvm)
        {
            HttpContext.Session.Remove(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags.Include(p => p.Tag),
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags.Include(p => p.Tag),
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
        }

        public IActionResult BlogSubBlogListNext(CKeyWordViewModel kyvm)
        {
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字))
                {
                    i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
                }
                if (i頁數 < _db.SubBlogs.Count() / i每頁筆數)
                {
                    i頁數++;
                }
                HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字, i頁數);
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags,
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags,
                    //subBlogs = _db.SubBlogs.Include(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
        }

        public IActionResult BlogSubBlogListPrevious(CKeyWordViewModel kyvm)
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
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags,
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags,
                    //subBlogs = _db.SubBlogs.Include(p=>p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                    subBlogs = _db.SubBlogs.Include(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
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
                if (i頁數 < (_db.Blogs.Count() / i每頁筆數) - 1)
                {
                    i頁數++;
                }
                HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字, i頁數);
                vm = new CBlogViewModel
                {
                    //blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubTag.Name.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    blogs = _db.Blogs.Include(p => p.SubTag).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubTag.Name.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    //blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
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
                    blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubTag.Name.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    //blogs = _db.Blogs.Include(p => p.SubTag).Where(b => b.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    subTags = _db.SubTags
                };
                return View(vm);
            }
        }

        public IActionResult BlogSubTagEdit(int? SId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                subTags = _db.SubTags.Where(P => P.SubTagId == SId).Select(a => a)
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult BlogSubTagEdit(SubTag Snart, int? SId)
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

        public IActionResult BlogEdit(int? BId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                blogs = _db.Blogs.Where(a => a.BlogId == BId).Select(a => a)

            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult BlogEdit(CAdminBlogViewModel Bnart, int? BId)
        {

            Blog q = _db.Blogs.FirstOrDefault(p => p.BlogId == BId);

            Blog dbblog = _db.Blogs.First(a => a.BlogId == Bnart.BlogId);

            if (dbblog != null)
            {
                if (Bnart.Photo != null)
                {
                    string photoName = Guid.NewGuid().ToString() + ".jpg";
                    q.FImagePath = photoName;
                    Bnart.Photo.CopyTo(new FileStream(_enviro.WebRootPath + "/Images/Blogimages/" + photoName, FileMode.Create));
                }
                dbblog.Title = Bnart.Title;
                _db.SaveChanges();
            }

            return RedirectToAction("BlogList");
        }

        public ActionResult BlogDelete(int? BId)
        {
            Blog dbblog = _db.Blogs.FirstOrDefault(a => a.BlogId == BId);
            if (dbblog != null)
            {
                dbblog.SubTagId = 14;
                _db.SaveChanges();
            }
            return RedirectToAction("BlogList");
        }


        public IActionResult BlogSubTagCreate(/*int? STId*/)
        {

            CBlogViewModel vm = null;

            vm = new CBlogViewModel
            {
                subTags = _db.SubTags
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult BlogSubTagCreate(SubTag sub)
        {
            _db.SubTags.Add(sub);
            _db.SaveChanges();
            return RedirectToAction("BlogCategoryList");
        }


        public IActionResult BlogCreate()
        {

            CBlogViewModel vm = null;

            vm = new CBlogViewModel
            {
                blogs = _db.Blogs.Include(a => a.SubTag).Where(a => a.SubTagId == 4),
                subTags = _db.SubTags.Where(a => a.Tag.TagId == 4)
            };
            return View(vm);
        }

        [HttpPost]

        public IActionResult BlogCreate(CAdminBlogViewModel bg)
        {
            string photoName = "";
            if (bg.Photo != null)
            {
                photoName = Guid.NewGuid().ToString() + ".jpg";
                bg.FImagePath = photoName;
                bg.Photo.CopyTo(new FileStream(_enviro.WebRootPath + "/Images/Blogimages/" + photoName, FileMode.Create));
            }

            var blog = new Blog
            {
                Title = bg.Title,
                SubTagId = bg.SubTagId,
                FImagePath = photoName,
            };
            _db.Blogs.Add(blog);
            _db.SaveChanges();
            var subblog = new SubBlog
            {
                Title = "綜合討論",
                BlogId = blog.BlogId,
            };
            _db.SubBlogs.Add(subblog);
            _db.SaveChanges();

            return RedirectToAction("BlogList");
        }




        public IActionResult BlogSubBlogCreate(/*int? STId*/)
        {

            CBlogViewModel vm = null;

            vm = new CBlogViewModel
            {

                blogs = _db.Blogs.Include(a => a.SubTag).Where(a => a.SubTag.TagId == 4),
                subTags = _db.SubTags.Where(a => a.Tag.TagId == 4),
                subBlogs = _db.SubBlogs.Include(a => a.Blog).Where(a => a.Blog.SubTag.TagId == 4)
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult BlogSubBlogCreate(SubBlog sub)
        {
            _db.SubBlogs.Add(sub);
            _db.SaveChanges();
            return RedirectToAction("BlogSubBlogList");
        }

        public ActionResult BlogSubBlogDelete(int? SBId)
        {
            SubBlog dbsblog = _db.SubBlogs.FirstOrDefault(a => a.SubBlogId == SBId);
            if (dbsblog != null)
            {
                dbsblog.BlogId = 17;
                _db.SaveChanges();
            }
            return RedirectToAction("BlogSubBlogList");
        }


        public IActionResult BlogSubBlogEdit(int? SBId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                subBlogs = _db.SubBlogs.Where(a => a.SubBlogId == SBId).Select(a => a)
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult BlogSubBlogEdit(SubBlog SBnart, int? SBId)
        {
            SubBlog dbSB = _db.SubBlogs.First(a => a.SubBlogId == SBnart.SubBlogId);
            if (dbSB != null)
            {
                dbSB.Title = SBnart.Title;
            }

            _db.SaveChanges();
            return RedirectToAction("BlogSubBlogList");
        }


        //public IActionResult BlogArticleList(CKeyWordViewModel kyvm)
        //{
        //    HttpContext.Session.Remove(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
        //    CBlogViewModel vm = null;
        //    if (string.IsNullOrEmpty(kyvm.txtKeyWord))
        //    {
        //        vm = new CBlogViewModel
        //        {
        //            blogs = _db.Blogs.Include(p => p.SubTag),
        //            subTags = _db.SubTags.Include(p => p.Tag),
        //            subBlogs = _db.SubBlogs.Include(p => p.Blog),
        //            articles = _db.Articles.Include(p=>p.SubBlog).ThenInclude(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
        //        };
        //        return View(vm);
        //    }
        //    else
        //    {
        //        vm = new CBlogViewModel
        //        {
        //            blogs = _db.Blogs.Include(p => p.SubTag),
        //            subTags = _db.SubTags.Include(p => p.Tag),
        //            subBlogs = _db.SubBlogs.Include(p => p.Blog),
        //            articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p=>p.Blog).Where(b =>b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) ||b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
        //        };
        //        return View(vm);
        //    }
        //}
        public IActionResult BlogArticleList(CKeyWordViewModel kyvm, int? FId)
        {
            HttpContext.Session.Remove(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags.Include(p => p.Tag),
                    subBlogs = _db.SubBlogs.Include(p => p.Blog),
                    articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
            else
            {
                if (FId == null)
                {
                    vm = new CBlogViewModel
                    {
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subTags = _db.SubTags.Include(p => p.Tag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                    };
                }
                else
                {
                    vm = new CBlogViewModel
                    {
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subTags = _db.SubTags.Include(p => p.Tag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => (b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)) && b.SubBlog.BlogId == FId).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                    };
                }
                return View(vm);
            }
        }
        public IActionResult BlogArticleListNext(CKeyWordViewModel kyvm, int? FId)
        {
            CBlogViewModel vm = null;
            if (string.IsNullOrEmpty(kyvm.txtKeyWord))
            {
                if (HttpContext.Session.Keys.Contains(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字))
                {
                    i頁數 = (int)HttpContext.Session.GetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字);
                }
                if (i頁數 < _db.Articles.Count() / i每頁筆數)
                {
                    i頁數++;
                }
                HttpContext.Session.SetInt32(CDictionary.SK_管理者觀看版面清單頁數使用關鍵字, i頁數);
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subTags = _db.SubTags,
                    subBlogs = _db.SubBlogs.Include(p => p.Blog),
                    articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
            else
            {
                if (FId == null)
                {
                    vm = new CBlogViewModel
                    {
                        subTags = _db.SubTags,
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    };
                }

                else
                {
                    vm = new CBlogViewModel
                    {
                        subTags = _db.SubTags,
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => (b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)) && b.SubBlog.BlogId == FId).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                    };
                }

                return View(vm);
            }
        }

        public IActionResult BlogArticleListPrevious(CKeyWordViewModel kyvm, int? FId)
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
                    subTags = _db.SubTags,
                    blogs = _db.Blogs.Include(p => p.SubTag),
                    subBlogs = _db.SubBlogs.Include(p => p.Blog),
                    articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Skip(i每頁筆數 * i頁數).Take(i每頁筆數),
                };
                return View(vm);
            }
            else
            {
                if (FId == null)
                {
                    vm = new CBlogViewModel
                    {
                        subTags = _db.SubTags,
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                    };
                }
                else
                {
                    vm = new CBlogViewModel
                    {
                        subTags = _db.SubTags,
                        blogs = _db.Blogs.Include(p => p.SubTag),
                        subBlogs = _db.SubBlogs.Include(p => p.Blog),
                        articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Where(b => (b.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Title.Contains(kyvm.txtKeyWord) || b.SubBlog.Blog.Title.Contains(kyvm.txtKeyWord)) && b.SubBlog.BlogId == FId).Skip(i每頁筆數 * i頁數).Take(i每頁筆數)
                    };
                }
                return View(vm);
            }
        }

        public ActionResult BlogArticleDelete(int? AFId)
        {
            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);

            if (art != null)
            {
                // 修改 Article 的 SubBlogID
                art.SubBlogId = 191;  // 新的 SubBlogID
                _db.SaveChanges();
            }
            return RedirectToAction("BlogArticleList");
        }



        public IActionResult BlogArticleComplainList()
        {
            CBlogViewModel vm = null;
            vm = new CBlogViewModel
            {
                articleComplain = _db.ArticleComplains.Include(p => p.Article).Include(p => p.Member).Include(p => p.SubTag).Select(a => a),
                articles = _db.Articles.Include(p => p.SubBlog).ThenInclude(p => p.Blog).Include(p => p.Member).Select(a => a),
                members = _db.Members,
                status = _db.Statuses,
            };

            return View(vm);
        }




        public IActionResult BlogArticleComplainFail(int? APId)
        {
            var q = from n in _db.ArticleComplains
                    where n.Id == APId
                    select n;
            var q1=_db.ArticleComplains.First(a => a.Id == APId);

            _db.ArticleComplains.Remove(q1);
            _db.SaveChanges();

            return Content("此篇檢舉非屬實，已移除此篇檢舉");
        }

        public IActionResult BlogArticleComplainSusscess(int? APId , int? AFId )
        {
            var q = from n in _db.ArticleComplains
                    where n.Id == APId
                    select n;
            var q1 = _db.ArticleComplains.First(a => a.Id == APId);

            _db.ArticleComplains.Remove(q1);
            _db.SaveChanges();

            //-----------


            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);

            if (art != null)
            {
                // 修改 Article 的 SubBlogID
                art.SubBlogId = 191;  // 新的 SubBlogID
                _db.SaveChanges();
            }
            //-----------

            




            return Content("此篇檢舉屬實，已刪除文章，並已移除此篇檢舉");
        }


        #endregion
        //---------------------------論壇---------------------------

        #region 委託Admin

        public IActionResult ADeputeList()
        {
            IEnumerable<CDeputeViewModel> datas = null;
            CDeputtListLoad x = new CDeputtListLoad(_host, _db);
            datas = x.List.OrderBy(n => n.id).ToList();
            return View(datas);
        }
        public IActionResult ADeputeEdit(CAdminDepute vm)
        {
            var data = _db.Deputes.Where(n => n.DeputeId == vm.txtID);

            foreach (var item in data)
            {
                //
                item.StatusId = vm.txtStatusID;
            }
            _db.SaveChanges();
            return RedirectToAction("ADeputeList");
        }
        public IActionResult ACDeputeList(CAdminDepute vm)
        {
            _db.Members.Load();
            _db.SubTags.Load();
            _db.Statuses.Load();
            _db.Deputes.Load();
            List<CDeputeComplainsWrap> list = new List<CDeputeComplainsWrap>();
            CDeputeComplainsWrap x = null;
            var datas = _db.DeputeComplains.OrderBy(n => n.Id);
            foreach (var item in datas) 
            {
                x = new CDeputeComplainsWrap();
                x.Id = item.Id;
                x.DeputeId = item.DeputeId;
                x.ProviderId = item.Depute.ProviderId;
                //x.ProviderStatus = item.Depute.Provider;//todo bian先註解
                //x.MemberId = item.MemberId;
                x.SubTagId = item.SubTag.Name;
                x.ReportContent = item.ReportContent;
                x.ReportDate = item.ReportDate;
                x.Status = item.Status.Name;
                list.Add(x);
            }
            ////

            return View(list);
        }
        public IActionResult ACDeputeEdit(CAdminDepute vm)
        {
            var data = _db.DeputeComplains.Where(n => n.Id == vm.txtID);

            foreach (var item in data)
            {
                item.StatusId = vm.txtStatusID;
            }
            _db.SaveChanges();
            return RedirectToAction("ACDeputeList");
        }
        public IActionResult ACDeputePenalties(CAdminDepute vm)
        {
            var data = _db.Members.Where(n => n.MemberId == vm.txtID);

            foreach (var item in data)
            {
                item.StatusId = vm.txtStatusID;
            }
            _db.SaveChanges();
            return RedirectToAction("ACDeputeList");
        }


        #endregion
    }
}
