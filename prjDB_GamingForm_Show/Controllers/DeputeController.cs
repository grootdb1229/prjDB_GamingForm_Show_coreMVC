using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
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
                    id = item.DeputeId.ToString(),
                    providername = item.Name,
                    startdate = item.SrartDate,
                    modifieddate = item.Modifiedate,
                    content = item.DeputeContent,
                    salary = item.Salary.ToString(),
                    status = item.Status,
                    region = item.City
                };

                List.Add(x);

            }
        }

        public IActionResult DeputeList(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = null;
            if (string.IsNullOrEmpty(vm.txtKeyword))
            {
                ListLoad();
                datas = from n in List
                        select n;

            }
            else
            {
                datas = List.Where(n => n.content.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.providername.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.salary.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower()) ||
                                          n.region.Trim().ToLower().Contains(vm.txtKeyword.Trim().ToLower())
                                          )
                   .OrderByDescending(n => n.modifieddate);

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
                            where n.content.Contains(item)
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

        

        public int memberIdtest = 38;
        public IActionResult test()
        {
            return View();
        }
        
        public IActionResult Personal()
        {
            return View();
        }
        public IActionResult PartialReleaseList()
        {
            _db.Deputes.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            _db.Skills.Load();
            var q = _db.Deputes.Where(_ => _.ProviderId == memberIdtest).Select(_ => _);
            return PartialView(q);
        }
        public IActionResult PartialReceiveList()
        {
            _db.Deputes.Load();
            _db.DeputeRecords.Load();
            _db.Regions.Load();
            _db.Statuses.Load();
            var q = _db.DeputeRecords.Where(_ => _.MemberId == memberIdtest).Select(_ => _);
            return PartialView(q);
        }
        public IActionResult PartialGallery()
        {
            return PartialView();
        }
    }
}
