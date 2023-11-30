using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Member;
using prjDB_GamingForm_Show.ViewModels;
using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http.Extensions;

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

        //public IActionResult Test() 
        //{
        //    return View();
        //}
        public IActionResult MemberPage(int? id)
        {
            //if (id == null)
            //    return RedirectToAction("Create");
            if (id == null)
            {
                return RedirectToAction("Login", "Home");
            }
            IEnumerable<Member> datas = null;
            datas = from m in _db.Members
                    where m.MemberId == id
                    select m;
            return View(datas);
        }


        public IActionResult Test(int? id)
        {
            IEnumerable<Member> datas = null;
            datas = from m in _db.Members
                    where m.MemberId == id
                    select m;
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CMemberWrap memberWrap)
        {
            Member member = new Member();
            member.Name = memberWrap.Name;
            member.Phone = memberWrap.Phone;
            member.Birth = memberWrap.Birth;
            member.Email = memberWrap.Email;
            member.Password = memberWrap.Password;
            member.Gender = memberWrap.Gender;
            string photoName = Guid.NewGuid().ToString() + ".jpg";
            member.FImagePath = photoName;
            memberWrap.photo.CopyTo(new FileStream(_host.WebRootPath + "/MemberPhoto/" + photoName, FileMode.Create));
            _db.Members.Add(member);
            _db.SaveChanges();
            ViewBag.ID = member.MemberId;
            return RedirectToAction("HomePage", "Home");
        }

        public IActionResult Edit(int? id)
        {
            id = HttpContext.Session.GetInt32("user_id");
            Member member = _db.Members.FirstOrDefault(p => p.MemberId == id);
            if (member == null)
                return RedirectToAction("MemberPage", "Member");
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
                dbmember.Email = member.Email;
                dbmember.Password = member.Password;
                _db.SaveChanges();
            }
            return RedirectToAction("MemberPage", "Member");
        }
        //會員忘記密碼流程
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
        public IActionResult SendEmail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
            message.To.Add(new MailboxAddress("alan90306", "alan90306@gmail.com"));
            message.Subject = "Test mail of asp.net Core";
            message.Body = new TextPart("html")
            {
                Text = "<!DOCTYPE html>" +
                        "<html>" +
                        "<h2> 電子信件已寄送 請至電子信箱進行確認 </h2>" +
                        "<h3> 您的驗證碼 及 會員編號 </h3>" +
                        "<p> 您的會員編號為:" + HttpContext.Session.GetInt32(CDictionary.SK_Confirmed_MemberID)+ "</p>" +
                        "<p> 您的驗證碼為:"   + @Guid.NewGuid() + "</p>" +
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
        public IActionResult ValidationPage()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult ValidationPage(CValidationViewModel CV)
        //{

        //}




    }
}
