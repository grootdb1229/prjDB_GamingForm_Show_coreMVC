using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Member;
using prjDB_GamingForm_Show.ViewModels;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Identity.Client;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MailKit.Search;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Controllers
{
    public class MemberController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public MemberController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region MemberCreate&MemberPage 
        public IActionResult MemberPageTest()
        {
            int id = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            if (id == null)
            {
                return RedirectToAction("Login", "Home");
            }
            _db.Members.Load();
            IEnumerable<Member> datas = null;
            var data = from m in _db.Members
                       where m.MemberId == id
                       select m;
            return View(data);
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(CMemberWrap memberWrap)
        {
            Member member = new Member();
            if (memberWrap.photo != null)
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                member.FImagePath = photoName;
                memberWrap.photo.CopyTo(new FileStream(_host.WebRootPath + "/MemberPhoto/" + photoName, FileMode.Create));
            }
            else 
            { 
                 member.FImagePath ="MemberDefault.jpg";
            }
            member.Name = memberWrap.Name;
            member.Phone = memberWrap.Phone;
            member.Birth = memberWrap.Birth;
            member.Email = memberWrap.Email;
            member.Password = memberWrap.Password;
            member.Mycomment = memberWrap.MyComment;
            member.Gender =memberWrap.Gender;
            _db.Members.Add(member);
            _db.SaveChanges();
            ViewBag.ID = member.MemberId;
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult Edit()
        {
            int id = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            Member member = _db.Members.FirstOrDefault(p => p.MemberId == id);
            if (member == null)
                return RedirectToAction("MemberPageTest", "Member");
            else
                return View(member);
        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            Member dbmember = _db.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if (member == null)
            {
                return RedirectToAction("Create");
            }
            //dbmember == null;
            if (dbmember != null)
            {
                dbmember.Name = member.Name;
                dbmember.Phone = member.Phone;
                dbmember.Birth = member.Birth;
                dbmember.Email = member.Email;
                dbmember.Gender = member.Gender;
                dbmember.Password = member.Password;
                _db.SaveChanges();
            }
            return RedirectToAction("MemberPageTest", "Member");
        }
        #endregion
        #region SendEmails
        public IActionResult SendEmail()
        {

            string ValGuid = new Guid().ToString("D");
            HttpContext.Session.SetString(CDictionary.SK_Validation_Guid, ValGuid);
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
            message.To.Add(new MailboxAddress("alan90306", "alan90306@gmail.com"));
            message.Subject = "Test mail of asp.net Core";
            message.Body = new TextPart("html")
            {
                Text = "<!DOCTYPE html>" +
                        "<html>" +
                        "<h2> 您的驗證碼 及 會員編號 </h3>" +
                        "<p> 您的會員編號為:" + HttpContext.Session.GetInt32(CDictionary.SK_Confirmed_MemberID)+ "</p>" +
                        "<p> 您的驗證碼為:"   + HttpContext.Session.GetString(CDictionary.SK_Validation_Guid) + "</p>" +
                        "</html>"
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("ValidationPage", "Member");
        }
        //public IActionResult SendOrderEmail(OrederProductViewModel VM)
        //{
        //    var message = new MimeMessage();
        //    message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
        //    message.To.Add(new MailboxAddress("alan90306", "alan90306@gmail.com"));
        //    message.Subject = "Your Order from GrootShopping";
        //    message.Body = new TextPart("html")
        //    {
        //        Text = "<!DOCTYPE html>" 
        //        +
        //                "<html>" +
        //                "<h2>  您的訂單資訊 </h2>" +
        //                "<p>   您的訂單編號編號為:" + VM.OrderID + "</p>" +
        //                "<p>   價格為:" + VM.Discount + "</p>" +
        //                "<p>   下單日期為:" + VM.ModifiedDate + "</p>" +
        //                "<p>   商品名稱為:" +VM.ProductNames  + "</p>" +
        //                "</html>"
        //    };
        //    用迴圈抓商品名稱 可能要抓折扣後的價格(折扣) 可能要抓(訂單編號) 下單時間
        //    using (var client = new SmtpClient())
        //    {
        //        client.Connect("smtp.gmail.com", 587, false);
        //        client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
        //        client.Send(message);
        //        client.Disconnect(true);
        //    }
        //    return RedirectToAction("Index", "Shop");
        //}
        #endregion
        #region PasswordValidation
        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ForgetPassword(CForgetPasswordViewModel CF)
        {
            var Emails = from e in _db.Members
                         select e.Email;
            //IQueryable<IEnumerable<char>> memberEmails = Emails;
            if (Emails.Contains(CF.txtConfirm_Email))
            {
                int Confirmed_MemberID = (from m in _db.Members
                                          where m.Email == CF.txtConfirm_Email
                                          select m.MemberId).FirstOrDefault();
                HttpContext.Session.SetInt32(CDictionary.SK_Confirmed_MemberID, Confirmed_MemberID);
                return RedirectToAction("SendEmail", "Member");
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        public IActionResult ValidationPage()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ValidationPage(CValidationViewModel CV)
        {
            Member dbMember = (from m in _db.Members
                             where m.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_Confirmed_MemberID)
                             select m).FirstOrDefault();
            if (dbMember == null) 
            {
                return RedirectToAction("ForgetPassword" , "Member");
            }
            if (dbMember != null) 
            {
                dbMember.Password = CV.txtVal_Password;
                _db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
        #endregion
        #region MemberDetails
        public IActionResult MyOrders() 
        {
            var data = _db.Orders.Include(O => O.OrderProducts).ThenInclude(P => P.Product)
                      .Where(O => O.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID))
                      .Select(O => O);
            return Json(data);
        }

        public IActionResult MyInfo() 
        {
            var data = from m in _db.Members
                       where m.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)
                       select new { m.Mycomment, m.Name, m.Phone, m.Gender, m.Birth.Year , m.FImagePath , m.Email};
            return Json(data);
        }
        public IActionResult MyCollection() 
        {
            var data = from c in _db.MemberCollections
                       where c.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)
                       select new { c.Title, c.ModifiedDate, c.FImagePath, c.Intro };
            return Json(data);
        }
        public IActionResult CreateCollection() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCollection(CMemberCollectionWrap CW) 
        {
            Member dbMember = (from m in _db.Members
                               where m.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)
                               select m).FirstOrDefault();
            if (dbMember == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else 
            {
                string photoName = Guid.NewGuid().ToString() + ".jpg";
                MemberCollection MC = new MemberCollection();
                MC.MemberId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
                MC.Title = CW.Title;
                MC.Intro = CW.Intro;
                MC.FImagePath = photoName;
                MC.MyCollection = CW.MyCollection;
                MC.ModifiedDate = (DateTime.Now).ToString();
                CW.photo.CopyTo(new FileStream(_host.WebRootPath + "/images/MemberCollectionPreView/" + photoName, FileMode.Create));
                _db.Add(MC);
                _db.SaveChanges();
                return RedirectToAction("MyCollections", "Member");
            }
        }
        public IActionResult EditCollection() 
        { 
           return View();
        }
        [HttpPost]
        public IActionResult EditCollection(MemberCollection memberCollection) 
        {
             MemberCollection dbCollection = _db.MemberCollections.FirstOrDefault
                             (mc => mc.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID));
            if (memberCollection == null)
            {
                return RedirectToAction("CreateCollection" , "Member");
            }
            if (memberCollection != null)
            {
                dbCollection.Title =  memberCollection.Title;
                dbCollection.Intro =  memberCollection.Intro;
                dbCollection.MyCollection = memberCollection.MyCollection;
                dbCollection.ModifiedDate = (DateTime.Now).ToString();
                _db.SaveChanges();
            }
            return RedirectToAction("MyCollections", "Member");
        }
        public IActionResult MyWorks() 
        {
            return View();
        }

        public IActionResult MyBlog() 
        {
            return View();
        }

        public IActionResult MyArticles() 
        {
            var data = _db.Articles.Include(a => a.SubBlog).ThenInclude(s => s.Blog)
                .Where(a => a.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID))
                .Select(a => a);
            var datas = from A in _db.Articles
                        where A.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)
                        select A;
            return Json(data);
        }
        public IActionResult MyWishList() 
        {
            return View();
        }
        //public IActionResult MakeAWish() 
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult MakeAWish() 
        //{
        //    return RedirectToAction("MyWishList", "Member");
        //}
        #endregion
        #region Tests
        //public IActionResult Test(int? id)
        //{
        //    IEnumerable<Member> datas = null;
        //    datas = from m in _db.Members
        //            where m.MemberId == id
        //            select m;
        //    return View(datas);
        //}
        //public IActionResult Test() 
        //{
        //    return View();
        //}
        //public IActionResult MemberPage(int? id)
        //{
        //    //if (id == null)
        //    //    return RedirectToAction("Create");
        //    if (id == null)
        //    {
        //        return RedirectToAction("Login", "Home");
        //    }
        //    _db.Members.Load();
        //    IEnumerable<Member> datas = null;
        //    var data = from m in _db.Members
        //               where m.MemberId == id
        //               select m;
        //    return View(data);
        //}
        //public bool EmailVal {  get; set; }
        //public bool PhoneVal { get; set; }
        //public bool PasswordVal { get; set; }
        //public IActionResult CreateValidation(CKeyWord vm) 
        //{
        // CMemberWrap datas = new CMemberWrap();
        // IEnumerable<string> Email = (from m in _db.Members.AsEnumerable()
        //            where m.Email.Equals(vm.txtEmail)
        //            select m.Email).ToList();


        // if (Email.Count()==0)
        // {
        //     EmailVal = true;
        //     datas.EmailValMsg = "該信箱可註冊";
        // }
        // else
        // {
        //     EmailVal = false;
        //     datas.EmailValMsg = "該信箱已被註冊";
        // }


        // IEnumerable<string> Phone = (from m in _db.Members.AsEnumerable()
        //                              where m.Email.Equals(vm.txtEmail)
        //                              select m.Email).ToList();
        // if (Phone.Count() == 0)
        // {
        //     PhoneVal = true;
        //     datas.PhoneValMsg = "該信箱可註冊";
        // }
        // else
        // {
        //     PhoneVal = false;
        //     datas.PhoneValMsg = "該信箱已被註冊";

        // }

        // IEnumerable<string> password = (from m in _db.Members.AsEnumerable()
        //                              where m.Email.Equals(vm.txtEmail)
        //                              select m.Email).ToList();
        // if (password.Count() == 0)
        // {
        //     PasswordVal = true;
        //     datas.PasswordValMsg = "該密碼可註冊";
        // }
        // else
        // {
        //     PasswordVal = false;
        //     datas.PasswordValMsg = "該密碼已被註冊";

        // }

        //if (EmailVal == true && PhoneVal == true && PasswordVal == true)
        //塞session
        //CDictionary.SK_Create_Validation = "true";
        //return View(datas);
        //}
        //會員忘記密碼流程
        #endregion
    }
}
