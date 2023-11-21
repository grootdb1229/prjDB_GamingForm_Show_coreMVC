using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Collections.Generic;
using static prjDB_GamingForm_Show.Controllers.DeputeController;

namespace prjDB_GamingForm_Show.Controllers
{
    public class DeputeController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public DeputeController(IWebHostEnvironment host, DbGamingFormTestContext context)
        {
            _host = host;
            _db = context;
            ListLoad();
        }
        public void ListLoad()
        {
            //test
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
                           Status = n.Status.Name,
                           n.Region.City
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
                    DeputeContent = item.DeputeContent,
                    salary = item.Salary,
                    status = item.Status,
                    region = item.City
                };

                List.Add(x);

            }
        }

        public IActionResult DeputeList()
        {
            
            return View();

        }
        public IActionResult Search(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword) &&
                vm.txtSkill == "請選擇..." && 
                vm.txtSkillClass == "請選擇..." && 
                vm.txtSalary == "請選擇...")
            {
                ListLoad();
                datas = from n in List
                        select n;

            }
            else
            {
                
                if (string.IsNullOrEmpty(vm.txtKeyword))
                    vm.txtKeyword = "";
                if (vm.txtSkill== "請選擇...")
                    vm.txtSkill = "";
                if (vm.txtSkillClass== "請選擇...")
                    vm.txtSkillClass = "";
                if (vm.txtSalary == "請選擇...")
                    vm.txtSalary = "0";
                datas = List.Where(n => (n.DeputeContent.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.providername.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.salary.ToString().Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.region.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()))
                                          &&
                                          (n.DeputeContent.Trim().ToLower().Contains(vm.txtSkill.Trim().ToLower()))
                                          &&
                                          (n.DeputeContent.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()))
                                          &&
                                          (n.salary>=(Convert.ToInt32(vm.txtSalary)))
                                          )
                   .OrderByDescending(n => n.modifieddate);

            }
            if(datas==null || datas.Count()==0)
            {
                return Content("No result");
            }
            return Json(datas);
        }

        //test
        public List<CDeputeViewModel> List { get; set; }
        
        public IActionResult Index()
        {
            //測試網頁樣板用
            return View();
        }
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
        public IActionResult DeputeCount()
        {
            var SkillClasses = _db.SkillClasses.Select(n => n.Name);
            List<CDeputeViewModel> slist = new List<CDeputeViewModel>();
            CDeputeViewModel x = null;
            foreach (var item in SkillClasses) 
            {
                var datas = from n in List.AsEnumerable()
                            where n.DeputeContent.Contains(item)
                            select n;

                x = new CDeputeViewModel()
                {
                    Skillname = item,
                    Count = datas.Count()
                };

                slist.Add(x);


            }
            return Json(slist);
        }

        public IActionResult NewFive()
        {
            var datas = List.OrderByDescending(n=>n.modifieddate).Take(5);
            return Json(datas);
        }

        public IActionResult TopFive(int? id)
        {
            var datas = _db.Skills.Where(a => a.SkillClassId == id);
            return Json(datas);
        }
        public IActionResult DeputeMain() 
        {
        
            return View();
        }

        

        public int _memberIdtest = 38;
        
        public IActionResult Apply(int id)
        {
            ViewBag.memberid = _memberIdtest;
            Depute o=_db.Deputes.FirstOrDefault(_ => _.DeputeId == id);
            if (o == null)
                return RedirectToAction("Index");
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
                return RedirectToAction("Personal");
            return View(n);
        }
        [HttpPost]
        public IActionResult Edit(CDeputeViewModel vm)
        {
            Depute o=_db.Deputes.Where(_=>_.DeputeId==vm.id).FirstOrDefault();
            if (o != null)
            {
                o.DeputeId = vm.id;
                o.ProviderId = _memberIdtest;
                o.StartDate = Convert.ToDateTime(vm.startdate);
                o.Modifiedate=DateTime.Now;
                o.DeputeContent = vm.DeputeContent;
                o.Salary = vm.salary;
                //o.StatusId = _db.Statuses.FirstOrDefault(_ => _.Name == vm.status).StatusId;
                //o.RegionId = _db.Regions.FirstOrDefault(_ => _.City == vm.region).RegionId;
                o.Title = vm.title;
                _db.SaveChanges();
            }
            return RedirectToAction("Personal");
        }
        public IActionResult SkillClasses()
        {
            var datas = new
            {
                skillclasses = _db.SkillClasses.Select(_ => _),
                skills = _db.Skills.Select(_ => _)
            };
            //var datas = _db.SkillClasses.Select(_ => _);
            return Json(datas);
        }
        public IActionResult Regions()
        {
            var datas=_db.Regions.Select(_ => _);
            return Json(datas);
        }
        public IActionResult Skillss(string skillClass)
        {
            int skillclassid = Convert.ToInt32(_db.SkillClasses.Where(_ => _.Name == skillClass).FirstOrDefault().SkillClassId);
            var datas = _db.Skills.Where(_ => _.SkillClassId == skillclassid).Select(_ => _);
            return Json(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CDeputeViewModel vm)
        {
            Depute n= new Depute()
            {
                ProviderId= _memberIdtest,
                StartDate= DateTime.Now,
                Modifiedate= DateTime.Now,
                DeputeContent= vm.DeputeContent,
                Salary= vm.salary,
                //StatusId = _db.Statuses.FirstOrDefault(_ => _.Name == vm.status).StatusId,
                //RegionId = _db.Regions.FirstOrDefault(_ => _.City == vm.region).RegionId,
                Title= vm.title,
            };
            _db.Deputes.Add(n);
            _db.SaveChanges();
            return RedirectToAction("Personal");
        }
        public IActionResult deleteDepute(int id)
        {
            Depute o=_db.Deputes.Where(_=>_.DeputeId==id).Select(_=>_).FirstOrDefault();
            _db.Deputes.Remove(o);
            _db.SaveChanges();
            return RedirectToAction("Personal");
        }
        
        public IActionResult Personal()
        {
            ViewBag.memberid=_memberIdtest;
            return View();
        }
        public IActionResult PartialReleaseList()
        {
            _db.Deputes.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            _db.Skills.Load();
            var q = _db.Deputes.Where(_ => _.ProviderId == _memberIdtest).Select(_ => _);
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
    }
}
