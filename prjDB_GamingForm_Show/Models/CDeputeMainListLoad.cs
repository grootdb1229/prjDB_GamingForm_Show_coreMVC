using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace prjDB_GamingForm_Show.ViewModels
{
    public class CDeputeMainListLoad
    {   
        public CDeputeMainListLoad() {  ListLoad(); ComboLoad(); ComboLoad1(); }
        public List<CDeputeViewModel> List { get; set; }
        public List<string> HotKeyList {  get; set; }
        public List<SelectListItem> ComboList { get; set; }
        public List<SelectListItem> ComboList1 { get; set; }
        public List<SelectListItem> ComboList2 { get; set; }
       

        // GET: Depute
        public void ListLoad()
        {
            List = new List<CDeputeViewModel>();
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            db.Deputes.Load();
            db.Members.Load();
            db.Statuses.Load();
            db.Regions.Load();
            var data = from n in db.Deputes.AsEnumerable()
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
                    id = item.DeputeId,
                    providername = item.Name,
                    startdate = item.SrartDate,
                    modifieddate = item.Modifiedate,
                    deputeContent = item.DeputeContent,
                    salary = item.Salary,
                    status = item.Status,
                    region = item.City
                };

                List.Add(x);

            }
        }

        public void HotKey()
        {
            HotKeyList = new List<string>();
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            db.SerachRecords.Load();
            var value = from n in db.SerachRecords.AsEnumerable()
                        group n by n.Name into q
                        orderby q.Count() descending
                        select q.Key;

            HotKeyList = value.ToList();




        }

        public void ComboLoad()
        {
            ComboList = new List<SelectListItem>();
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var value = from n in db.SkillClasses.AsEnumerable()
                        select n;
            ComboList.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = "default",
                Selected = true
            });

            foreach (var item in value)
            {

                ComboList.Add(new SelectListItem() { Text = item.Name,Value = item.Name.ToString(),Selected= false });

            }
            
        }
        
        public void ComboLoad1()
        {
            ComboList1 = new List<SelectListItem>();
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var value = from n in db.Regions.AsEnumerable()
                        select n;
            ComboList1.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = "default",
                Selected = true
            });
            foreach (var item in value)
            {

                ComboList1.Add(new SelectListItem() { Text = item.City, Value = item.RegionId.ToString(), Selected = false });

            }

        }
        public void ComboLoad2()
        {
            ComboList = new List<SelectListItem>();
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var value = from n in db.Deputes.AsEnumerable()
                        select n.Region.City;
            ComboList.Add(new SelectListItem()
            {
                Text = "請選擇",
                Value = "default",
                Selected = true
            });

            
        }
  

    }
}