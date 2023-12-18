using DB_GamingForm_Show.Job.DeputeClass;
using Elfie.Serialization;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Org.BouncyCastle.Bcpg;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Collections.Generic;
using System.Linq;
using MailKit;
using MailKit.Net.Smtp;
using System.Text.Json;
using System.Web;
using static prjDB_GamingForm_Show.Controllers.DeputeController;
using static System.Runtime.InteropServices.JavaScript.JSType;
using OpenAI_API;
using OpenAI_API.Models;
using Azure;
using OpenAI_API.Chat;
using Microsoft.AspNetCore.Http.Extensions;
using Org.BouncyCastle.Ocsp;
using System.Collections;

namespace prjDB_GamingForm_Show.Controllers
{
    public class DeputeController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public List<CDeputeViewModel> List { get; set; }
        public List<CDeputeViewModel> Temp { get; set; }
        public List<CDeputeViewModel> CookieList { get; set; }

        public string[] MutipleKeywords { get; set; }
        public DeputeController(IWebHostEnvironment host, DbGamingFormTestContext context)
        {
            _host = host;
            _db = context;
            ListLoad();
        }
        #region 老朱

        //TODO #1 讀資料//
        public void ListLoad()
        {
            //te
            Temp = new List<CDeputeViewModel>();
            List = new List<CDeputeViewModel>();
            _db.Members.Load();
            _db.Statuses.Load();
            _db.Regions.Load();
            var data = from n in _db.Deputes.AsEnumerable()
                       orderby n.StartDate descending
                       select new
                       {
                           n.DeputeId,
                           n.Title,
                           Name = n.Provider.Name,
                           SrartDate = n.StartDate.ToString("d"),
                           Modifiedate = n.Modifiedate.ToString("d"),
                           n.DeputeContent,
                           n.Salary,
                           n.ViewCount,
                           Status = n.Status.Name,
                           n.Region.City,
                           n.Provider.FImagePath
                       };
            CDeputeViewModel x = null;

            foreach (var item in data)
            {
                x = new CDeputeViewModel()
                {
                    id = item.DeputeId,
                    title = item.Title,
                    providername = item.Name,
                    startdate = item.SrartDate,
                    modifieddate = item.Modifiedate,
                    deputeContent = item.DeputeContent,
                    salary = item.Salary,
                    viewcount = item.ViewCount,
                    status = item.Status,
                    region = item.City,
                    imgfilepath = item.FImagePath
                };

                List.Add(x);
                Temp = List;
            }
        }

        public IActionResult DeputeList(int? id)
        {

            IEnumerable<CDeputeViewModel> datas = null;
            if (id ==null)
            {
                //ListLoad();
                datas = from n in List
                        select n;
                
            }
            else
            {
                _db.SkillClasses.Load();
                IEnumerable<string> skillname = from n in _db.SkillClasses
                                where n.SkillClassId == id
                                select n.Name;
                foreach (string item in skillname)
                {
                    datas = from n in List
                            where n.deputeContent.Contains(item)
                            select n;
                }
                
               
            }
            return View(datas);

        }
        public IActionResult DetailSkills(int? id)
        {
            
            _db.SkillClasses.Load();
            IEnumerable<string> skillname = (from n in _db.DeputeSkills
                                            where n.DeputeId == id
                                            select n.Skill.Name).Distinct();
            return Json(skillname);

        }
        public IActionResult MutipleSearch(CKeyWord vm)
        {
            Temp = List;
            IEnumerable<CDeputeViewModel> datas = null;
            if (vm.txtMutiKeywords != null)
            {
                foreach (var item in vm.txtMutiKeywords)
                {
                    if (!string.IsNullOrEmpty(item))
                    { 
                        _db.SerachRecords.Add(new SerachRecord { Name = item, CreateDays = (DateTime.Now.Date) });
                        _db.SaveChanges();
                    }

                    datas = Temp.Where(n => (n.deputeContent.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                               n.title.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                               n.providername.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                               n.region.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                               n.status.Trim().ToLower().Contains(item.Trim().ToLower())
                                               ))
                                               .OrderByDescending(n => n.modifieddate);
                    Temp = datas.ToList();

                }
            }
            SelectedSearch(vm);
            Orderby(vm);
            return Json(Temp);


        }

        private void SelectedSearch(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = Temp.Where(n =>
                            (DateTime.Now.Date - Convert.ToDateTime(n.modifieddate)).Days <= vm.txtDate &&
                            n.viewcount >= vm.txtView &&
                            n.salary >= vm.txtSalary);
            Temp = datas.ToList();
        }

        public void Orderby(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = null;
            switch (vm.txtOrderby)
            {
                case 1:
                    datas = Temp.OrderByDescending(n =>n.salary);
                    break;
                case 2:
                    datas = Temp.OrderByDescending(n => Convert.ToDateTime(n.modifieddate));
                    break;
                case 3:
                    datas = Temp.OrderByDescending(n => n.viewcount);
                    break;
                default:
                    datas = Temp;
                    break;
            }
            if(vm.txtEsc)
                datas = datas.Reverse();
            Temp = datas.ToList();
        }
        public void Move(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = null;
           
           datas = Temp.Skip(10).Take(10);
           Temp = datas.ToList();
        }
        //TODO #3 委託詳細
        public IActionResult DeputeDetails(int? id)
        {
            Cookie(id);
            CDeputeViewModel pln = null;
            Depute pDb = _db.Deputes.FirstOrDefault(n => n.DeputeId == id);

            if (pDb != null)
            {

                Depute data = (from n in _db.Deputes
                               where n.DeputeId == id
                               select n).First();
                data.ViewCount += 1;
                _db.SaveChanges();


                pln = new CDeputeViewModel();
                pln.id = pDb.DeputeId;
                pln.providername = pDb.Provider.Name;
                pln.title = pDb.Title;
                pln.startdate = pDb.StartDate.ToString("yyyy/mm/dd HH:mm:ss");
                pln.modifieddate = pDb.Modifiedate.ToString("yyyy/mm/dd HH:mm:ss");
                pln.deputeContent = pDb.DeputeContent;
                pln.salary = pDb.Salary;
                pln.status = pDb.Status.Name;
                pln.region = pDb.Region.City;
                pln.memeberContent = pDb.Provider.Mycomment;
                pln.imgfilepath = pDb.Provider.FImagePath;

            }

            return View(pln);


        }
        //檢舉//////
        public IActionResult DeputeComplain(CAdminDepute vm)
        {
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
            { 
            _db.DeputeComplains.Add(
                new DeputeComplain
                {
                    DeputeId = vm.txtID,
                    MemberId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID),
                    ReportContent = vm.txtReportContent,
                    ReportDate = (DateTime.Now),
                    SubTagId = vm.txtSubTagID
                }
                );
            _db.SaveChanges();
            }
            return View();
        }

        //Cookie
        public void Cookie(int? id)
        {
            int userid =0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID)!=null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            
            string record = "";
            if (HttpContext.Request.Cookies[userid.ToString()] != null)
                record = HttpContext.Request.Cookies[userid.ToString()];

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            record += $"{id},";
            HttpContext.Response.Cookies.Append(userid.ToString(), record, options);//

        }
        public IActionResult GetCookie()
        {
            CookieList = new List<CDeputeViewModel>();
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            string record = "";
            if (HttpContext.Request.Cookies[userid.ToString()] != null)
                record = HttpContext.Request.Cookies[userid.ToString()];
            string[] strResult = record.Split(',');
            strResult = strResult.Reverse().Distinct().ToArray();
            IEnumerable<CDeputeViewModel> datas = null;
            foreach (var item in strResult)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    datas = from n in List
                            where n.id == Convert.ToInt32(item)
                            select n;
                    foreach (var data in datas) 
                    {
                        CookieList.Add(data);
                    }
                }
                
            }
            return Json(CookieList.Take(5));
        }

        public IActionResult Fav(int? id)
        {
            
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);

            string record = "";
            if (HttpContext.Request.Cookies[$"fav{userid}"] != null)
                record = HttpContext.Request.Cookies[$"fav{userid}"];

            string[] strResult = record.Split(',');
            if (strResult.Contains(id.ToString()))
            {
                record = "";
                HttpContext.Response.Cookies.Delete($"fav{userid}");
                var data = strResult.Where(n => n != id.ToString());
                foreach (var item in data)
                {
                    if(!string.IsNullOrEmpty(item))
                        record += item.ToString() + ",";
                }
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Append($"fav{userid}", record, options);////
                return Content("False");
            }
            else
            { 
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            record += $"{id},";
            HttpContext.Response.Cookies.Append($"fav{userid}", record, options);//

            return Content("Succeed");
            }
        }
        public IActionResult GetFav()
        {
            CookieList = new List<CDeputeViewModel>();
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            string record = "";
            if (HttpContext.Request.Cookies[$"fav{userid}"] != null)
                record = HttpContext.Request.Cookies[$"fav{userid}"];
            string[] strResult = record.Split(',');
            strResult = strResult.Reverse().Distinct().ToArray();
            IEnumerable<CDeputeViewModel> datas = null;
            foreach (var item in strResult)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    datas = from n in List
                            where n.id == Convert.ToInt32(item)
                            select n;
                    foreach (var data in datas)
                    {
                        CookieList.Add(data);
                    }
                }

            }
            return Json(CookieList.Take(5));
        }
        //TODO #4 熱門關鍵字

        public IActionResult Recommand(int id)
        {
            var value = (from n in _db.DeputeSkills.AsEnumerable()
                         where n.DeputeId == id
                         select new { n.Skill.Name }).Distinct();
            List< CDeputeViewModel> Rcolist = new List< CDeputeViewModel>();
            CDeputeViewModel data = null;
            foreach(var item in value)
            {
                  var  RecoData = from n in _db.DeputeSkills.AsEnumerable()
                               where n.Skill.Name.Contains(item.Name)
                               select new {n.DeputeId,n.Depute.Title, n.Depute.DeputeContent, n.Depute.Provider.Name, n.Depute.Provider.FImagePath };
                CDeputeViewModel x = null;
                foreach (var item2 in RecoData)
                {

                        x = new CDeputeViewModel()
                        {
                            id = item2.DeputeId,
                            title = item2.Title,
                            providername = item2.Name,
                            deputeContent = item2.DeputeContent,
                            imgfilepath = item2.FImagePath
                        };
                    Rcolist.Add(x);
                }

            }

            Random rnd = new Random();
            int count = rnd.Next(0, 50);
            List<CDeputeViewModel> Rcolist2 = new List<CDeputeViewModel>();
            for (int i = 1; i <= 6; i++)
            {
                Rcolist2.Add(Rcolist[count]);
                Rcolist.RemoveAt(count);
                count = rnd.Next(0, Rcolist.Count);

            }
            return Json(Rcolist2);


        }
        public IActionResult HotKey(int id)
        {
            var value = (from n in _db.SerachRecords.AsEnumerable()
                         group n by n.Name into q
                         orderby q.Count() descending
                         select q.Key).Take(id);

            if (value == null)
                return RedirectToAction("DeputeList");

            return Json(value);


        }

        //測試網頁樣板用
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Index2()
        {

            return View();
        }
        //多選載入//
        public IActionResult SkillClassess()
        {
            var datas = from n in _db.SkillClasses
                        select n;
            return Json(datas);
        } 
        public IActionResult Skills(int? id)
        {
            var datas = _db.Skills.Where(a => a.SkillClassId == id);
            return Json(datas);
        }
        public IActionResult Region()
        
        {
            List<string> regions = new List<string>();
            var datas = from n in _db.Regions
                        select n.City;
            regions = datas.ToList();
            return Json(regions);
        }
        public IActionResult Status()
        {
            var datas = from n in List
                        select n.status;
            return Json(datas.Distinct());
        }

        //首頁類別跟數量
        public IActionResult DeputeCount()
        {
            var SkillClasses = _db.SkillClasses;
            List<CDeputeViewModel> slist = new List<CDeputeViewModel>();
            CDeputeViewModel x = null;
            foreach (var item in SkillClasses)
            {
                var datas = from n in List.AsEnumerable()
                            where n.deputeContent.Contains(item.Name)
                            select n;

                x = new CDeputeViewModel()
                {
                    skillid = item.SkillClassId,
                    skillname = item.Name,
                    count = datas.Count()
                };

                slist.Add(x);


            }
            return Json(slist);
        }

        //熱門5最新5
        public IActionResult GetFive(int? id)
        {
            IEnumerable<CDeputeViewModel> datas = null;
            if (id == 1)
            {
                datas = List.OrderByDescending(n => Convert.ToDateTime(n.startdate)).Take(5);
            }
            else
            {
                datas = List.OrderByDescending(n => n.viewcount).Take(5);
                
            }
            return Json(datas);
        }

        public IActionResult DeputeMain()
        {

            return View();
        }

        #endregion

        #region 老邊

        #region notAPI
        public IActionResult Create()
        {
            //判斷是否被封鎖
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
            {
                int memberStatus = _db.Members.FirstOrDefault(_ => _.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).StatusId;
                HttpContext.Session.SetInt32(CDictionary.SK_會員狀態編號, memberStatus);
            }
            return View();
        }
        [HttpPost]
        public IActionResult Create(CDeputeViewModel vm)
        {
            try
            {
                Depute n = new Depute()
                {
                    ProviderId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID),
                    StartDate = DateTime.Now,
                    Modifiedate = DateTime.Now,
                    DeputeContent = vm.deputeContent,
                    Salary = vm.salary,
                    StatusId = 18,//懸賞中
                    RegionId = _db.Regions.FirstOrDefault(_ => _.City == vm.region).RegionId,
                    Title = vm.title,
                };
                _db.Deputes.Add(n);
                _db.SaveChanges();

                //存該委託所需的技能(多類別)
                List<CDeputeSkillViewModel> list = JsonSerializer.Deserialize<List<CDeputeSkillViewModel>>(vm.skilllist);

                DeputeSkill ndsk = new DeputeSkill();
                foreach (var item in list)
                {
                    int skillclassID = _db.SkillClasses.FirstOrDefault(_ => _.Name == item.skillclass).SkillClassId;
                    int skillID = _db.Skills.FirstOrDefault(_ => _.SkillClassId == skillclassID && _.Name == item.skill).SkillId;
                    ndsk.Id = 0;
                    ndsk.DeputeId = n.DeputeId;
                    ndsk.SkillId = skillID;
                    _db.DeputeSkills.Add(ndsk);
                    _db.SaveChanges();
                }
                return Json(new { success = true, message = "委託發佈成功" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult Apply(int id)
        { 
            Depute o = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            if (o == null)
                return RedirectToAction("deputemain");

            //登入後跳轉前一頁
            ViewBag.preUrl = HttpContext.Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(Request.Cookies["ppreUrl"]))
            {
                //如果未登入點選狀態為例外項的應徵時執行此處
                ViewBag.preUrl = Request.Cookies["ppreUrl"];
                Response.Cookies.Delete("ppreUrl");
            }
            //判斷是否被封鎖
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
            {
                int memberStatus = _db.Members.FirstOrDefault(_ => _.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).StatusId;
                HttpContext.Session.SetInt32(CDictionary.SK_會員狀態編號, memberStatus);
            }
            return View(o);
        }
        [HttpPost]
        public IActionResult Apply(DeputeRecord data)
        {
            try
            {
                _db.DeputeRecords.Add(data);
                _db.SaveChanges();
                //有會員應徵後，寄給發佈委託者
                var currentDepute = _db.Deputes.FirstOrDefault(_ => _.DeputeId == data.DeputeId);
                CDeputeEmail emaiContent = new CDeputeEmail()
                {
                    memberName = _db.Members.FirstOrDefault(_ => _.MemberId == currentDepute.ProviderId).Name,
                    email = _db.Members.FirstOrDefault(_ => _.MemberId == currentDepute.ProviderId).Email,
                    deputeTitle = currentDepute.Title,
                    deputeStartDate = currentDepute.StartDate.ToString("yyyy/MM/dd"),
                    deputeStatus = currentDepute.Status.Name,
                    deputeRecordCount = _db.DeputeRecords.Count(_ => _.DeputeId == currentDepute.DeputeId),
                    progress = CDictionary.PROGRESS_會員應徵委託
                };
                SendDeputeEmail(emaiContent);
                return Json(new { success = true, message = "履歷投遞成功" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        public IActionResult Edit(int id)
        {
            _db.Deputes.Load();
            Depute n = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            
            if (n == null)
                return RedirectToAction("HomeFrame");
            return View(n);
        }
        [HttpPost]
        public IActionResult Edit(CDeputeViewModel vm)
        {
            Depute o = _db.Deputes.Where(_ => _.DeputeId == vm.id).FirstOrDefault();
            //前端radio，有勾選開放，沒勾選關閉
            int statusid = vm.statusid;
            if (statusid != 18)
                statusid = 19;
            if (o != null)
            {
                o.ProviderId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
                o.StartDate = Convert.ToDateTime(vm.startdate);
                o.Modifiedate = DateTime.Now;
                o.DeputeContent = vm.deputeContent;
                o.Salary = vm.salary;
                o.StatusId = statusid;
                o.RegionId = _db.Regions.FirstOrDefault(_ => _.City == vm.region).RegionId;
                o.Title = vm.title;
                _db.SaveChanges();
            }
            //刪除所有原技能
            _db.DeputeSkills.RemoveRange(_db.DeputeSkills.Where(_ => _.DeputeId == vm.id).Select(_ => _));
            
            //存技能
            List<CDeputeSkillViewModel> list = JsonSerializer.Deserialize<List<CDeputeSkillViewModel>>(vm.skilllist);

            DeputeSkill ndsk = new DeputeSkill();
            foreach (var item in list)
            {
                int skillclassID = _db.SkillClasses.FirstOrDefault(_ => _.Name == item.skillclass).SkillClassId;
                int skillID = _db.Skills.FirstOrDefault(_ => _.SkillClassId == skillclassID && _.Name == item.skill).SkillId;
                ndsk.Id = 0;
                ndsk.DeputeId = vm.id;
                ndsk.SkillId = skillID;
                _db.DeputeSkills.Add(ndsk);
                _db.SaveChanges();
            }
            return RedirectToAction("HomeFrame");
        }

        public IActionResult DeleteDepute(int id)
        {
            //partialrelease-刪除
            Depute o = _db.Deputes.Where(_ => _.DeputeId == id).Select(_ => _).FirstOrDefault();
            var dro = _db.DeputeRecords.Where(_ => _.DeputeId == id).Select(_ => _);
            var dso = _db.DeputeSkills.Where(_ => _.DeputeId == id).Select(_ => _);
            if (dro != null)
            {
                foreach (var item in dro)
                {
                    _db.DeputeRecords.Remove(item);
                }
            }
            if (dso != null)
            {
                foreach (var item in dso)
                {
                    _db.DeputeSkills.Remove(item);
                }
            }
            _db.Deputes.Remove(o);
            _db.SaveChanges();
            return RedirectToAction("HomeFrame");
        }
        public IActionResult DeleteDeputeRecord(int id)
        {
            //partialreceive-撤回
            DeputeRecord o = _db.DeputeRecords.FirstOrDefault(_ => _.Id == id);
            _db.DeputeRecords.Remove(o);
            _db.SaveChanges();
            return RedirectToAction("HomeFrame");
        }
        public IActionResult ReplyDepute(int id)
        {
            //受委託者完成委託
            var data = _db.DeputeRecords.FirstOrDefault(_ => _.Id == id);
            return View(data);
        }
        [HttpPost]
        public IActionResult ReplyDepute(CDeputeViewModel vm,IFormFile formFile)
        {
            //受委託者完成委託
            try
            {
                string fileType = formFile.FileName.Split('.')[1];
                string fileName = Guid.NewGuid().ToString() + "." + fileType;
                string filePath = Path.Combine(_host.WebRootPath, "files\\depute", fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
                var oriDeputRecord = _db.DeputeRecords.FirstOrDefault(_ => _.Id == vm.id);
                oriDeputRecord.ReplyContent = vm.replyContent;
                oriDeputRecord.ReplyFileName = fileName;
                oriDeputRecord.ApplyStatusId = 25;//狀態改為已完成(待確認)
                _db.SaveChanges();

                CDeputeEmail content = new CDeputeEmail()
                {
                    memberName=oriDeputRecord.Depute.Provider.Name,
                    workerName=oriDeputRecord.Member.Name,
                    email=oriDeputRecord.Depute.Provider.Email,
                    deputeTitle= oriDeputRecord.Depute.Title,
                    deputeStatus=oriDeputRecord.ApplyStatus.Name,
                    progress=CDictionary.PROGRESS_會員完成委託
                };
                SendDeputeEmail(content);
                return Json(new { success = true, message = "案件已提交" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion

        #region API

        private async Task<string> ChatAsync(string system,string user,double temp)
        {
            OpenAIAPI api = new OpenAIAPI("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");
            var chat = api.Chat.CreateConversation();
            chat.Model = Model.ChatGPTTurbo;
            chat.RequestParameters.Temperature = temp;

            chat.AppendSystemMessage($"{system}");
            chat.AppendUserInput($"{user}");
            return await chat.GetResponseFromChatbotAsync();
            //Console.WriteLine(response);
            //foreach (ChatMessage msg in chat.Messages)
            //{
            //    Console.WriteLine($"{msg.Role}: {msg.Content}");
            //}
        }
        public async Task<IActionResult> createContentAsync(string title)
        {
            string systemRule = "你將看到遊戲製作委託平台上的委託主題，你的工作是根據主題創造一篇50字左右的徵才委託。";
            var response = await ChatAsync(systemRule, title, 1.0);
            return Content(response);
        }
        public async Task<IActionResult> selectSkillAsync(string title,string content)
        {
            string systemRule = "你將看到一件委託主題及委託內容，" +
                "你的工作是根據以下列表中的技能，以JSON形式提供你的答案，" +
                "只能從此提供的技能清單中選擇最多5項skill(嚴禁新建任何skill):\r\n" +
                "skill：Csharp\r\nHtml\r\nCss\r\nLINQ\r\nADONET\r\nSQL\r\nJS\r\n電繪\r\n手繪\r\n水彩\r\n油畫\r\nJava\r\nPython\r\nPHP\r\nRuby\r\nASP.NET\r\nSwift\r\nKotlin\r\nReact\r\nSolidity\r\nSelenium\r\nJUnit\r\n電子\r\n搖滾\r\n古典\r\n爵士\r\n民族\r\n流行\r\n懸疑\r\n環境\r\n8位元\r\n16位元\r\nMaya\r\nPhotoShop\r\nPreminum";
            string userContent = "主題:" + title + "，" + "內容:" + content;
            var response = await ChatAsync(systemRule, userContent, 0);
            
            return Content(response);
        }

        public IActionResult SendDeputeEmail(CDeputeEmail vm)
        {
            string diffContent = "";
            string diffTable = "";
            string email = "bute77889@gmail.com";
            switch (vm.progress)
            {
                case CDictionary.PROGRESS_會員應徵委託:
                    diffContent = $"您發佈的委託「{vm.deputeTitle}」有新的應徵者，目前共有　{vm.deputeRecordCount}　位會員向您投遞履歷";
                    diffTable = "<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">委託標題</td><td style=\"border: 1px solid #ccc; padding: 8px;\">發佈日期</td><td style=\"border: 1px solid #ccc; padding: 8px;\">應徵人數</td></tr>" +
                        $"<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeTitle}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeStartDate}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeRecordCount}</td></tr></tbody></table></li>";
                    break;
                case CDictionary.PROGRESS_委託者決定合作:
                    diffContent = $"您應徵的委託「{vm.deputeTitle}」委託者已決定與您合作";
                    diffTable = "<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">委託標題</td><td style=\"border: 1px solid #ccc; padding: 8px;\">委託內容</td><td style=\"border: 1px solid #ccc; padding: 8px;\">目前狀態</td></tr>" +
                        $"<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeTitle}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeContent}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.recordStatus}</td></tr></tbody></table></li>";
                    break;
                case CDictionary.PROGRESS_會員完成委託:
                    diffContent = $"您發佈的的委託「{vm.deputeTitle}」狀態已更新為「{vm.deputeStatus}」";
                    diffTable = "<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">委託標題</td><td style=\"border: 1px solid #ccc; padding: 8px;\">執行會員</td><td style=\"border: 1px solid #ccc; padding: 8px;\">目前狀態</td></tr>" +
                        $"<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeTitle}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.workerName}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.recordStatus}</td></tr></tbody></table></li>";
                    break;
                case CDictionary.PROGRESS_委託者確認完成:
                    diffContent = $"您的委託「{vm.deputeTitle}」委託者已確認完成";
                    diffTable = "<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">委託標題</td><td style=\"border: 1px solid #ccc; padding: 8px;\">委託內容</td><td style=\"border: 1px solid #ccc; padding: 8px;\">目前狀態</td></tr>" +
                        $"<tr><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeTitle}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeContent}</td><td style=\"border: 1px solid #ccc; padding: 8px;\">{vm.deputeStatus}</td></tr></tbody></table></li>";
                    break;
            }
            //頂標(免改)
            string dm = "<div style=\"color:black;\">\r\n<ul style=\"list-style-type: none; padding-left: 0;\">  <li style=\"background-color: #272727; color: #fff; padding: 10px; margin-left: 0px;\">Groot遊戲資源整合平台</li>";
            //抬頭
            dm += $"<li style=\"margin: 10px 0;padding: 10px;\">　　<p>{vm.memberName}　您好，</p>{diffContent}，立即<a href=\"#\" style=\"color: #0d6efd; text-decoration: none;\">查看委託詳情</a>。</li>";
            dm += "<li style=\"margin: 10px 0;\"><table style=\"width: 100%; border-collapse: collapse;padding: 10px;\"><tbody>";
            //表格
            dm += $"{diffTable}";
            //頁尾(免改)
            dm += "<li style=\"margin: 10px 0;\"><a href=\"#\" style=\"background-color: #272727; color: #fff; padding: 10px; text-decoration: none; display: inline-block;border-radius:10px\">詳細資訊</a></li><li style=\"margin: 10px 0;\"><div style=\"margin-bottom: 10px;padding: 10px;\">Groot將依個人資料保護法及相關法令之規定下，依隱私權保護政策蒐集、處理及合理利用您的個人資料。</div><div style=\"margin-bottom: 10px;padding: 10px;\">為確保能收到來自Groot的通知信件，強烈建議您將groot1229@gmail.com加入通訊錄。</div></li></ul></div>";

            var message = new MimeMessage();
            //寄件者
            message.From.Add(new MailboxAddress("grootdb1229", "grootdb1229@gmail.com"));
            //收件者
            message.To.Add(new MailboxAddress(_db.Members.FirstOrDefault(x => x.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Name, email));
            //標題
            message.Subject = $"委託[{vm.deputeTitle}]狀態更新";
            //內容
            message.Body = new TextPart("html")
            {
                Text = dm 
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("grootdb1229@gmail.com", "fmgx uucs lgkv vqxm");
                client.Send(message);
                client.Disconnect(true);
            }
            return RedirectToAction("OrderDetail", "Shop");
        }
        public IActionResult myApplyContent(int id)
        {
            var o = _db.DeputeRecords.Where(_ => _.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Select(_ => new
            {
                content=_.RecordContent
            }).Distinct();
            return Json(o);
        }
        public IActionResult downloadFile(string fileName)
        {
            string fullFilePath= Path.Combine(_host.WebRootPath, "files\\depute", fileName);
            return PhysicalFile(fullFilePath, "application/octet-stream");
        }

        public IActionResult confirmApply(int id)
        {
            var ori = _db.DeputeRecords.FirstOrDefault(_ => _.DeputeId == id && _.ApplyStatusId == 25);
            CDeputeViewModel data = new CDeputeViewModel()
            {
                id = ori.Id,
                title = ori.Depute.Title,
                replyContent = ori.ReplyContent,
                replyFileName = ori.ReplyFileName,
            };
            return Json(data);
        }

        public IActionResult changeDeputeRecordStatus(string jsonString)
        {
            //partialrelease使用3處
            CDeputeViewModel vm = JsonSerializer.Deserialize<CDeputeViewModel>(jsonString);

            var deputeRecord = _db.DeputeRecords.FirstOrDefault(_ => _.Id == vm.id);
            var depute = _db.Deputes.FirstOrDefault(_ => _.DeputeId == deputeRecord.DeputeId);
            var otherRecords = _db.DeputeRecords.Where(_ => _.DeputeId == depute.DeputeId && _.Id != vm.id).ToList();

            //修改該會員應徵狀態
            deputeRecord.ApplyStatusId = vm.statusid;

            switch (vm.statusid)
            {
                case 10:
                    handleStatus10(depute, deputeRecord, otherRecords);
                    break;
                case 16:
                    handleStatus16(depute, deputeRecord);
                    break;
            }

            _db.SaveChanges();
            var statusName = deputeRecord.ApplyStatus.Name;
            return Content(statusName);
        }
        private void handleStatus10(Depute depute,DeputeRecord deputeRecord,List<DeputeRecord> otherRecords)
        {
            //若與該會員合作，則此委託狀態一併改為合作中，且其他會員的應徵狀態改為備選
            depute.StatusId = 10;
            deputeRecord.ApplyStatusId = 10;

            CDeputeEmail content = new CDeputeEmail()
            {
                memberName = deputeRecord.Member.Name,
                email = deputeRecord.Member.Email,
                deputeTitle = depute.Title,
                deputeContent = depute.DeputeContent,
                recordStatus = deputeRecord.ApplyStatus.Name,
                progress = CDictionary.PROGRESS_委託者決定合作
            };
            SendDeputeEmail(content);
            foreach (var item in otherRecords)
            {
                item.ApplyStatusId = 11;
            }
        }
        private void handleStatus16(Depute depute, DeputeRecord deputeRecord)
        {
            //完成委託、委託紀錄
            depute.StatusId = 16;
            deputeRecord.ApplyStatusId = 16;

            CDeputeEmail content = new CDeputeEmail()
            {
                memberName = deputeRecord.Member.Name,
                email = deputeRecord.Member.Email,
                deputeTitle = depute.Title,
                deputeStatus = deputeRecord.ApplyStatus.Name,
                progress = CDictionary.PROGRESS_委託者確認完成
            };
            SendDeputeEmail(content);
        }
        public IActionResult individualDetials(int id)
        {
            //發出的委託詳細資料
            var o = _db.DeputeRecords.Where(_ => _.DeputeId == id).Select(_ => _).Include(_ => _.Depute).ToList();
            List<CDeputeViewModel> list = new List<CDeputeViewModel>();
            foreach (var item in o)
            {
                CDeputeViewModel n = new CDeputeViewModel();
                n.id = item.Id;
                n.workerId = item.MemberId;
                n.title = item.Depute.Title;
                n.count = o.Count();
                n.status = item.ApplyStatus.Name;
                n.memberName = item.Member.Name;
                n.applyerGender = item.Member.Gender == 1 ? "男性" : "女性";
                n.applyerEmail = item.Member.Email;
                n.applyerBirth = item.Member.Birth.ToString("yyyy/MM/dd");
                n.applyerContent = item.RecordContent == null ? "該會員無提供補充資訊" : item.RecordContent;
                n.applyerPhone = item.Member.Phone;
                list.Add(n);
            }
            return Json(list);
        }
        public IActionResult DeputeDetial(int? id)
        {
            //收到委託詳細資料
            _db.DeputeRecords.Load();
            Depute o = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            if (o == null)
                return RedirectToAction("HomeFrame");
            CDeputeViewModel n = new CDeputeViewModel()
            {
                title = o.Title,
                status = o.Status.Name,
                count = o.DeputeRecords.Count(),
                deputeContent = o.DeputeContent,
                //imgfilepath=,
                providername = o.Provider.Name,
                region = o.Region.City,
                salary = o.Salary,
                startdate = o.StartDate.ToString("yyyy/MM/dd"),
                modifieddate = o.Modifiedate.ToString("yyyy/MM/dd"),
            };
            return Json(n);
        }

        public IActionResult oriSkills(int deputeID)
        {
            //該委託資料庫技能
            var datas = _db.DeputeSkills
                .Where(_ => _.DeputeId == deputeID)
                .Select(_ => _)
                .Include(_ => _.Skill)
                .ThenInclude(_ => _.SkillClass)
                .Select(item => new CDeputeSkillViewModel
                {
                    skill = item.Skill.Name,
                    skillclass = item.Skill.SkillClass.Name
                });
            return Json(datas);
        }

        public IActionResult SkillClasses()
        {
            //資料庫技能類別
            var datas = new
            {
                skillclasses = _db.SkillClasses.Select(_ => _),
                skills = _db.Skills.Select(_ => _)
            };
            return Json(datas);
        }
        public IActionResult editDeputeStatuses(int id)
        {
            var depute = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            if (depute == null)
                return NotFound();
            int oriStatusID = depute.StatusId;
            return Json(oriStatusID);
        }
        public IActionResult Regions()
        {
            //資料庫區域
            var datas = _db.Regions.Select(_ => new
            {
                _.City,
                _.RegionId
            });
            return Json(datas);
        }

        public IActionResult Skillss(string skillClass)
        {
            if (_db.SkillClasses.Where(_ => _.Name == skillClass) == null)
                return Content("");
            int skillclassid = Convert.ToInt32(_db.SkillClasses.Where(_ => _.Name == skillClass).FirstOrDefault().SkillClassId);
            var datas = _db.Skills.Where(_ => _.SkillClassId == skillclassid).Select(_ => _);
            return Json(datas);
        }
        #endregion

        #region MainView
        public IActionResult HomeFrame()
        {
            ViewBag.memberid = HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            ViewBag.HomeFrame = "HomeFrame";
            return View();
        }
        #endregion

        #region PartialView

        public IActionResult PartialOverview()
        {
            var release = _db.Deputes
                .Where(_ => _.ProviderId == HttpContext.Session.GetInt32(CDictionary.SK_UserID))
                .Select(depute => new
                {
                    RunCount = depute.DeputeRecords.Count(_ => _.ApplyStatusId == 10),
                    ApplyCount = depute.DeputeRecords.Count(_ => _.ApplyStatusId == 5),
                    ReplyCount = depute.DeputeRecords.Count(_ => _.ApplyStatusId == 25),
                    ComCount = depute.DeputeRecords.Count(_ => _.ApplyStatusId == 16),
                }).ToList();

            var receive = _db.DeputeRecords
                .Where(_ => _.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID))
                .Select(_ => _);
               
            if (release == null && receive.Count() == 0)
                return Content("尚無資料");

            var datas = new CDeputeOverViewModel();
            foreach (var item in release)
            {
                datas.runCount += item.RunCount;
                datas.applyCount += item.ApplyCount;
                datas.replyCount += item.ReplyCount;
                datas.comCount += item.ComCount;
            }
            datas.workerNewCount = receive.Count(_ => _.ApplyStatusId == 9);
            datas.workerRunCount = receive.Count(_ => _.ApplyStatusId == 10 || _.ApplyStatusId == 20);
            datas.workerCofirmCount = receive.Count(_ => _.ApplyStatusId == 25);
            datas.workerComCount = receive.Count(_ => _.ApplyStatusId == 16);

            return PartialView(datas);
        }
        public IActionResult PartialReleaseList()
        {
            _db.Deputes.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            _db.Skills.Load();
            _db.DeputeRecords.Load();
            //使用include:為計算該筆委託被應徵的次數(出現在deputerecord的次數)
            var q = _db.Deputes.Where(_ => _.ProviderId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Select(_ => _).Include(_ => _.DeputeRecords);
            return PartialView(q);
        }
        public IActionResult PartialReceiveList()
        {
            _db.Deputes.Load();
            _db.DeputeRecords.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            var q = _db.DeputeRecords.Where(_ => _.MemberId == HttpContext.Session.GetInt32(CDictionary.SK_UserID)).Select(_ => _);
            return PartialView(q);
        }
        public IActionResult PartialGallery()
        {
            return PartialView();
        }
        public IActionResult Contact()
        {
            return PartialView();
        }
        #endregion

        #endregion
    }
}
