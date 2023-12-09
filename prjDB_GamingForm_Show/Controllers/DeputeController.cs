﻿using DB_GamingForm_Show.Job.DeputeClass;
using Elfie.Serialization;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Bcpg;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Web;
using static prjDB_GamingForm_Show.Controllers.DeputeController;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        //TODO #1 讀資料
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
        //TODO #2 搜尋

        //public IActionResult Search(CKeyWord vm)
        // {
        //    IEnumerable<CDeputeViewModel> datas = null;
        //    if (string.IsNullOrEmpty(vm.txtKeyword) && (!string.IsNullOrEmpty(vm.txtHotkey)))
        //        vm.txtKeyword = vm.txtHotkey;

        //    if (string.IsNullOrEmpty(vm.txtKeyword))
        //    {
        //        ListLoad();
        //        datas = from n in List
        //                select n;
        //    }
        //    else
        //    {
        //        _db.SerachRecords.Add
        //                        (new SerachRecord { Name = vm.txtKeyword, CreateDays = (DateTime.Now.Date)});
        //        _db.SaveChanges();

        //        datas = List.Where(n => (n.deputeContent.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
        //                                  n.providername.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
        //                                  n.title.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
        //                                  n.region.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()))
        //                                  )
        //        .OrderByDescending(n => n.modifieddate);

        //    }
        //    if (datas == null || datas.Count() == 0)
        //    {
        //        return Content("No result");
        //    }
        //    return Json(datas);
        //}

        //public IActionResult SearchById(int? id)
        //{
        //    IEnumerable<CDeputeViewModel> datas = null;
        //    IEnumerable<string> keyword = from n in _db.SerachRecords
        //                  where n.Id == id
        //                  select n.Name;

        //    foreach (string item in keyword)
        //    {
        //        if(string.IsNullOrEmpty(item)) 
        //            continue;
        //        datas = List.Where(n => (n.deputeContent.Trim().ToLower().Contains(item.Trim().ToLower()) ||
        //                                  n.providername.Trim().ToLower().Contains(item.Trim().ToLower()) ||
        //                                  n.title.Trim().ToLower().Contains(item.Trim().ToLower()) ||
        //                                  n.region.Trim().ToLower().Contains(item.Trim().ToLower()))
        //                                  ).OrderByDescending(n => n.modifieddate);

        //    }
        //    if (datas == null || datas.Count() == 0)
        //    {
        //        return Content("No result");
        //    }

        //    return Json(datas);


        //}
        public IActionResult MutipleSearch(CKeyWord vm)
        {
            Temp = List;
            IEnumerable<CDeputeViewModel> datas = null;
            if (vm.txtMutiKeywords == null)
                return Json(List);
            foreach (var item in vm.txtMutiKeywords)
            {
                if (string.IsNullOrEmpty(item))
                 return Content("沒有符合的條件");

               datas = Temp.Where(n => (n.deputeContent.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                          n.title.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                          n.providername.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                          n.region.Trim().ToLower().Contains(item.Trim().ToLower())
                                          ))
                                          .OrderByDescending(n => n.modifieddate);
                Temp = datas.ToList();

            }
            if (Temp.Count==0)
            {
                return Json(Temp);
            }
            else
            {
                return Json(Temp);
            }
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


        //TODO #4 熱門關鍵字
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

        public IActionResult Index()
        {
            //測試網頁樣板用
            return View();
        }
        //下拉
        public IActionResult SkillClassess()
        {
            var datas = from n in _db.SkillClasses
                        select n;
            return Json(datas);
        }
        public IActionResult Region()
        {
            var datas = from n in _db.Regions
                        select n;
            return Json(datas);
        }
        public IActionResult Skills(int? id)
        {
            var datas = _db.Skills.Where(a => a.SkillClassId == id);
            return Json(datas);
        }
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

        //首頁
        public IActionResult GetFive(int? id)
        {
            if (id == 1)
            { 
            var datas = List.OrderByDescending(n => n.modifieddate).Take(5);
            return Json(datas);
            }
            else
            {
                var datas = List.OrderByDescending(n => n.viewcount).Take(5);
                return Json(datas);
            }
        }

        public IActionResult DeputeMain()
        {

            return View();
        }

        #endregion

        #region 老邊

        public int _memberIdtest = 38;
        public IActionResult changeDeputeRecordStatus(string deputerecordstatus)
        {
            CDeputeViewModel n = JsonSerializer.Deserialize<CDeputeViewModel>(deputerecordstatus);
            var o = _db.DeputeRecords.FirstOrDefault(_ => _.Id == n.id);
            int statusID = Convert.ToInt32(n.statusid);
            if (_db.Statuses.Any(_ => _.StatusId == statusID))
                o.ApplyStatusId = Convert.ToInt32(statusID);
            _db.SaveChanges();

            var statusName = _db.Statuses.FirstOrDefault(_ => _.StatusId == statusID).Name;
            return Content(statusName);
        }
        public IActionResult individualDetials(int id)
        {
            var o = _db.DeputeRecords.Where(_ => _.DeputeId == id).Select(_ => _).Include(_ => _.Depute).ToList();
            List<CDeputeViewModel> list = new List<CDeputeViewModel>();
            foreach (var item in o)
            {
                CDeputeViewModel n = new CDeputeViewModel();
                n.id = item.Id;
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
        public IActionResult Apply(int id)
        {
            ViewBag.memberid = _memberIdtest;
            Depute o = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            if (o == null)
                return RedirectToAction("deputemain");
            return View(o);
        }
        [HttpPost]
        public IActionResult Apply(DeputeRecord vm)
        {
            _db.DeputeRecords.Add(vm);
            _db.SaveChanges();
            return RedirectToAction("DeputeMain");
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
            if (o != null)
            {
                o.ProviderId = _memberIdtest;
                o.StartDate = Convert.ToDateTime(vm.startdate);
                o.Modifiedate = DateTime.Now;
                o.DeputeContent = vm.deputeContent;
                o.Salary = vm.salary;
                o.StatusId = _db.Statuses.FirstOrDefault(_ => _.Name == vm.status).StatusId;
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
        public IActionResult SkillClasses()
        {
            var datas = new
            {
                skillclasses = _db.SkillClasses.Select(_ => _),
                skills = _db.Skills.Select(_ => _)
            };
            return Json(datas);
        }
        public IActionResult editDeputeStatuses(int id)
        {
            List<CDeputeViewModel> statusList = new List<CDeputeViewModel>();
            int oriStatusID = _db.Deputes.FirstOrDefault(_ => _.DeputeId == id).StatusId;

            CDeputeViewModel oriStatus = new CDeputeViewModel()
            {
                id = oriStatusID,
                status =_db.Statuses.FirstOrDefault(_=>_.StatusId== oriStatusID).Name,
            };
            statusList.Add(oriStatus);
            
            var datas = _db.Statuses.Where(_ => (_.StatusId == 16 ||
            _.StatusId == 18 ||
            _.StatusId == 19 )&&
            _.StatusId != oriStatusID).Select(_ => _);

            foreach(var item in datas)
            {
                CDeputeViewModel lastStatuses = new CDeputeViewModel()
                {
                    id = item.StatusId,
                    status= item.Name,
                };
                statusList.Add(lastStatuses);
            };
            return Json(statusList);
        }
        public IActionResult Regions()
        {
            var datas = _db.Regions.Select(_ => _);
            return Json(datas);
        }
        
        public IActionResult Skillss(string skillClass)
        {
            if (_db.SkillClasses.Where(_ => _.Name == skillClass).FirstOrDefault() == null)
                return Content("");
            int skillclassid = Convert.ToInt32(_db.SkillClasses.Where(_ => _.Name == skillClass).FirstOrDefault().SkillClassId);
            var datas = _db.Skills.Where(_ => _.SkillClassId == skillclassid).Select(_ => _);
            return Json(datas);
        }
        public IActionResult oriSkills(int deputeID)
        {
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

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CDeputeViewModel vm)
        {
            Depute n = new Depute()
            {
                ProviderId = _memberIdtest,
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
            return RedirectToAction("HomeFrame");
        }
        public IActionResult DeleteDeputeRecord(int id)
        {
            DeputeRecord o = _db.DeputeRecords.FirstOrDefault(_ => _.Id == id);
            _db.DeputeRecords.Remove(o);
            _db.SaveChanges();
            return RedirectToAction("HomeFrame");
        }
        public IActionResult DeleteDepute(int id)
        {
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
        public IActionResult DeputeDetial(int? id)
        {
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

        #region MainView
        public IActionResult HomeFrame()
        {
            ViewBag.memberid = _memberIdtest;
            return View();
        }
        #endregion

        #region PartialView


        public IActionResult PartialOverview()
        {
            return PartialView();
        }
        public IActionResult PartialReleaseList()
        {
            _db.Deputes.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            _db.Skills.Load();
            _db.DeputeRecords.Load();
            //使用include:為計算該筆委託被應徵的次數(出現在deputerecord的次數)
            var q = _db.Deputes.Where(_ => _.ProviderId == _memberIdtest).Select(_ => _).Include(_ => _.DeputeRecords);
            return PartialView(q);
        }
        public IActionResult PartialReceiveList()
        {
            _db.Deputes.Load();
            _db.DeputeRecords.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            var q = _db.DeputeRecords.Where(_ => _.MemberId == _memberIdtest).Select(_ => _);
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
