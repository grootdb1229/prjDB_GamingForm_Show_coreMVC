using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;
using prjDB_GamingForm_Show.ViewModels;
using System.Collections.Generic;

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
        }
        //test
        public List<CDeputeViewModel> List { get; set; }
        public List<string> HotKeyList { get; set; }
        public List<SelectListItem> ComboList { get; set; }
        public List<SelectListItem> ComboList1 { get; set; }
        public List<SelectListItem> ComboList2 { get; set; }
        public IActionResult Index()
        {
            return View();
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
        
        public ActionResult DeputeList(CKeyWord vm)
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
            
            return View(datas);

        }
        public IActionResult test()
        {
            return View();
        }
    }
}
