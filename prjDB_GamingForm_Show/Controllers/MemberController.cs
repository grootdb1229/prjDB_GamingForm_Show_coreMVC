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
using Microsoft.AspNetCore.Components.Web;
using System.Net.Mail;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using static prjDB_GamingForm_Show.Hubs.MemberChatHub;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace prjDB_GamingForm_Show.Controllers
{
    public class MemberController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public MemberController(IWebHostEnvironment host, DbGamingFormTestContext db )
        {
            _host = host;
            _db = db;    
        }

        public IActionResult MemberInfo() 
        {
            return PartialView("MemberInfo");
        }
        public IActionResult Index()
        {
            return View();
        }
        #region MemberCreate&MemberPage 
        public IActionResult MemberPageTest(int? id)
        {
            int MemberID = 0;
            if (id != null)
                MemberID = (int)id;
            else
            {
                if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                    MemberID = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            }
            _db.Members.Load();
            IEnumerable<Member> datas = null;
            var data = from m in _db.Members
                       where m.MemberId == MemberID
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
                member.FImagePath = "MemberDefault.jpg";
            }
            member.Name = memberWrap.Name;
            member.Phone = memberWrap.Phone;
            member.Birth = memberWrap.Birth;
            member.Email = memberWrap.Email;
            member.Password = memberWrap.Password;
            member.Mycomment = memberWrap.MyComment;
            member.Gender = memberWrap.Gender;
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
        public async Task<IActionResult> SendAdvertisments() 
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SendAdvertisments(CEmail email)
        {
            
            List<string> EmailData = (from E in _db.Members
                                      where E.Email.Contains("alan")
                                      select E.Email).ToList();
            List<string> Emails = EmailData;
            Emails.Add("alan90306@gmail.com");
            Emails.Add("alan90306@gmail.com");
            email.Emails = Emails;
            foreach (string Address in email.Emails)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
                message.To.Add(new MailboxAddress("alan90306", Address));
                message.Subject = email.EmailSubject;
                message.Body = new TextPart("html")
                {
                    Text = email.EmailBody
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            return RedirectToAction("Home", "HomePage");
        }



        #region SendEmailByModel
        public IActionResult SendEmailByModel()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmailByModel(CEmail email)
        {
            List<string> EmailData = (from E in _db.Members
                                      where E.Email.Contains("alan")
                                      select E.Email).ToList();
            List<string> Emails = EmailData;
            Emails.Add("alan90306@gmail.com");
            Emails.Add("alan90306@gmail.com");
            email.Emails = Emails;
            foreach (string Address in email.Emails)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
                message.To.Add(new MailboxAddress("alan90306", Address));
                message.Subject = email.EmailSubject;
                message.Body = new TextPart("html")
                {
                    Text = email.EmailBody

                };
               
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
                    client.Send(message);
                    client.Disconnect(true);
                }
            }
            return RedirectToAction("HomePage", "Home");
        }
        #endregion
        #region SendEmail V1
        //Send ValidationEmail
        public IActionResult SendEmail()
        {
            int ValNumber = new Random().Next(1000,10000);
            HttpContext.Session.SetInt32(CDictionary.SK_Validation_Number , ValNumber);
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
            return RedirectToAction("ValidationPage", "Member");
        }
         public void SendEmail(string EmailAddress) 
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
                return RedirectToAction("ForgetPassword", "Member");
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
        public IActionResult MyInfo(int? id)
        {
            var data = from m in _db.Members
                       where m.MemberId == id
                       select new { m.Mycomment, m.Name, m.Phone, m.Gender, m.Birth.Year, m.FImagePath, m.Email , m.Password};
            return Json(data);
        }

        public IActionResult MemInfoForMaui() 
        {
            var datas = from m in _db.Members
                        select new { m.Name, m.Email, m.Password , m.Phone , m.Gender, m.Birth.Year };
            return Json(datas);
        }
        public IActionResult MyCollectionList(int? id)
        {
            var data = from c in _db.MemberCollections
                       where c.MemberId == id
                       select c;
            return View(data);
        }
        public IActionResult DeputeInfo(int? id) 
        {
            var data = _db.DeputeRecords.Include(d => d.Depute).Where(d1 => d1.MemberId == id).Select(d => d);
          return Json(data);
        }
        public IActionResult MyCollection(int? id) 
        {
            var data = from mc in _db.MemberCollections
                       where mc.MemberId == id
                       select new { mc.Title, mc.FImagePath, mc.Intro, mc.ModifiedDate };
            return Json(data);  
        }
        public IActionResult MyCollectionView(int? CId)
        {
            var data = from c in _db.MemberCollections
                       where c.Id == CId
                       select c;
            return View(data);
        }

        public IActionResult Test2() 
        {
            return View();
        }
        #region CollectionCRUD
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
                return RedirectToAction("MyCollectionList", "Member" , new {id=MC.MemberId});
            }
        }
        public IActionResult EditCollection(int? CId)
        {
            MemberCollection memberCollection = _db.MemberCollections.First(m => m.Id == CId);
            //new MemberCollection()
            //{
            //    MyCollection = _db.MemberCollections.Where(a => a.Id == CId).FirstOrDefault().
            //};
            return View(memberCollection);
        }
        [HttpPost]
        public IActionResult EditCollection(MemberCollection memberCollection)
        {
            MemberCollection MC = _db.MemberCollections.First(c => c.Id == memberCollection.Id);
            if (MC == null)
            {
                return RedirectToAction("CreateCollection", "Member");
            }
            if (memberCollection != null)
            {
                MC.Title = memberCollection.Title;
                MC.Intro = memberCollection.Intro;
                MC.MyCollection = memberCollection.MyCollection;
                MC.ModifiedDate = (DateTime.Now).ToString();
                _db.SaveChanges();
            }
            return RedirectToAction("MyCollectionList", "Member", new {id=memberCollection.MemberId});
        }
        public IActionResult DeleteCollection(int? CId)
        {
            MemberCollection MC = _db.MemberCollections.First(c => c.Id == CId);
            if (MC != null) 
            { 
               _db.MemberCollections.Remove(MC);
               _db.SaveChanges();
            }
            return RedirectToAction("MyCollectionList", "Member", new { id = MC.MemberId });
        }
        
        #endregion
        #region MyHistoryRecord
        public IActionResult MyWorks()
        {
            return View();
        }
        public IActionResult MyArticleList(int? id) 
        {
            return View();
        }
        public IActionResult MyArticles(int? id)
        {
            var data = _db.Articles.Include(a => a.SubBlog).ThenInclude(s => s.Blog)
                .Where(a => a.MemberId == (int)id)
                .Select(a => a);
            var datas = from A in _db.Articles
                        where A.MemberId == id
                        select A;
            return Json(data);
        }
        public IActionResult MyDeputeList(int? id) 
        {
            return View();
        }
        public IActionResult DeputeApplying(int? id) 
        {
            return View();
        }
        public IActionResult ReturnMyApply(int? id) 
        {
            _db.Statuses.Load();
            _db.Regions.Load();
            var data = _db.DeputeRecords.Include(D=>D.Depute)
                       .Where(D=>D.MemberId == id).Select(D=>D);
            return Json(data);
        }
        public IActionResult MyDeputes(int? id)
        {
            var data = new
            {
                DeputeRecords = _db.DeputeRecords.Where(d => d.MemberId == id),
                DeputeR1 = _db.Deputes.SelectMany(d => d.DeputeRecords.Where(c => c.MemberId == id)),
            };
            return Json(data);
        }
        public IActionResult MyWishList()
        {
            return View();
        }
        #endregion
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
        #region SignalRChatForMember - 彥霖
        public IActionResult OpenChat()
        {
            List<CPublicChatUseViewModel> chats = new List<CPublicChatUseViewModel>();
            foreach(var chat in _db.PublicChats)
            {
                CPublicChatUseViewModel ori = new CPublicChatUseViewModel()
                {
                    SenderName = _db.Members.Where(m => m.MemberId == chat.SenderId).Select(m => m.Name).FirstOrDefault(),
                    Content = chat.ChatContent,
                    ModefiedDate = chat.Modifiedate
                };
                chats.Add(ori);
            }
            return View(chats);
        }

        public bool IsLoginOrNot()
        {
            if (HttpContext.Session.GetString(CDictionary.SK_UserName) != null)
            {
                return true;
            }
            else { return false; }
        }

        public IActionResult ChatMemberList()
        {
            List<CSignalRUseMemberList> members = new List<CSignalRUseMemberList>();
            var datas = from a in _db.Members
                        select new
                        {
                            a.MemberId,
                            a.Name
                        };
            foreach (var data in datas)
            {
                CSignalRUseMemberList member = new CSignalRUseMemberList();
                member.MemberId = data.MemberId;
                member.MemberName = data.Name;
                members.Add(member);
            }

            return PartialView(members);
        }

        public string GetMemberImg(string name)
        {
            string imgPath = _db.Members.Where(m => m.Name == name).Select(m => m.FImagePath).FirstOrDefault();
            return imgPath;
        }

        public int GetMemberId(string name)
        {
            int id = _db.Members.Where(m => m.Name == name).Select(m => m.MemberId).FirstOrDefault();
            return id;
        }

        public IActionResult ChatPV(string recevier)
        {
            var senderid = _db.Members.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_UserName)).Select(a => a.MemberId).FirstOrDefault();
            var recevierid = _db.Members.Where(a => a.Name == recevier).Select(a => a.MemberId).FirstOrDefault();
            List<CMemberChatUseViewModel> chats = new List<CMemberChatUseViewModel>();
            ViewBag.SendToName = recevier;
            foreach (var chat in _db.MemberChats)
            {
                if (chat.SenderMember == senderid && chat.ReceiveMember == recevierid)
                {
                    CMemberChatUseViewModel ori = new CMemberChatUseViewModel()
                    {
                        SenderName = HttpContext.Session.GetString(CDictionary.SK_UserName),
                        ReceiverName = recevier,
                        SenderId = senderid,
                        ReceiverId = recevierid,
                        SenderImg = _db.Members.Where(a => a.MemberId == senderid).Select(a => a.FImagePath).FirstOrDefault(),
                        ReceiverImg = _db.Members.Where(a => a.MemberId == recevierid).Select(a => a.FImagePath).FirstOrDefault(),
                        ChatContent = chat.ChatContent,
                        ModefiedDate = chat.ModefiedDate,
                        IsCheck = chat.IsCheck
                    };
                    chats.Add(ori);
                }
                else if (chat.SenderMember == recevierid && chat.ReceiveMember == senderid)
                {
                    CMemberChatUseViewModel ori = new CMemberChatUseViewModel()
                    {
                        SenderName = recevier,
                        ReceiverName = HttpContext.Session.GetString(CDictionary.SK_UserName),
                        SenderId = recevierid,
                        ReceiverId = senderid,
                        SenderImg = _db.Members.Where(a => a.MemberId == senderid).Select(a => a.FImagePath).FirstOrDefault(),
                        ReceiverImg = _db.Members.Where(a => a.MemberId == recevierid).Select(a => a.FImagePath).FirstOrDefault(),
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
                CMemberChatUseViewModel newchat = new CMemberChatUseViewModel()
                {
                    SenderName = HttpContext.Session.GetString(CDictionary.SK_UserName),
                    ReceiverName = recevier,
                    SenderId = senderid,
                    ReceiverId = recevierid,
                    SenderImg = _db.Members.Where(a => a.MemberId == senderid).Select(a => a.FImagePath).FirstOrDefault(),
                    ReceiverImg = _db.Members.Where(a => a.MemberId == recevierid).Select(a => a.FImagePath).FirstOrDefault(),
                };
                chats.Add(newchat);
                return PartialView(chats);
            }
        }

        [HttpPost]
        public int HowMuchMessageNotCheck()
        {
            int count = 0;
            int reid = _db.Members.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_UserName)).Select(a => a.MemberId).FirstOrDefault();
            foreach (var m in _db.MemberChats)
            {
                if (m.ReceiveMember == reid && m.IsCheck == false)
                {
                    count++;
                }
            }
            return count;
        }

        public IActionResult NotCheckMessage()
        {
            int reid = _db.Members.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_UserName)).Select(a => a.MemberId).FirstOrDefault();
            List<CAdminNotCheckMessageViewModel> vm = new List<CAdminNotCheckMessageViewModel>();
            CAdminNotCheckMessageViewModel message = null;
            foreach (var m in _db.MemberChats)
            {
                if (m.ReceiveMember == reid && m.IsCheck == false)
                {
                    message = new CAdminNotCheckMessageViewModel()
                    {
                        senderName = _db.Members.Where(a => a.MemberId == m.SenderMember).Select(a => a.Name).FirstOrDefault(),
                        senderImgPath = _db.Members.Where(a => a.MemberId == m.SenderMember).Select(a => a.FImagePath).FirstOrDefault(),
                        message = m.ChatContent,
                        sendTime = MessageTime(m.ModefiedDate),
                        senderId = _db.Members.Where(a => a.MemberId == m.SenderMember).Select(a => a.MemberId).FirstOrDefault()
                    };
                    vm.Add(message);
                }
            }
            return Json(vm);
        }

        public string MessageTime(string time)
        {
            DateTime messagetime = DateTime.Parse(time);
            DateTime now = DateTime.Now.ToLocalTime();
            TimeSpan timeSpan = now - messagetime;
            if (timeSpan.TotalDays >= 1)
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

        [HttpPost]
        public void CheckAllMessage(int senderid)
        {
            int reid = _db.Members.Where(a => a.Name == HttpContext.Session.GetString(CDictionary.SK_UserName)).Select(a => a.MemberId).FirstOrDefault();
            foreach (var m in _db.MemberChats)
            {
                if (m.ReceiveMember == reid && m.SenderMember == senderid && m.IsCheck == false)
                {
                    m.IsCheck = true;
                }
            }
            _db.SaveChanges();
        }
        #endregion
    }
}
