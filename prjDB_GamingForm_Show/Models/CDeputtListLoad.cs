using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.CallBack.Depute;
using prjDB_GamingForm_Show.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace prjDB_GamingForm_Show.ViewModels
{
    public class CDeputtListLoad
    {
        public List<CDeputeViewModel> List { get; set; }
        public List<CDeputeViewModel> Temp { get; set; }

        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        private readonly CDeputeDataLoad _dataLoad;
        public CDeputtListLoad
            (
            IWebHostEnvironment host,
            DbGamingFormTestContext db,
            CDeputeDataLoad dataLoad
            )
        {
            _host = host;
            _db = db;
            _dataLoad = dataLoad;
        }

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
                           n.Provider.Name,
                           SrartDate = n.StartDate.ToString("d"),
                           Modifiedate = n.Modifiedate.ToString("d"),
                           n.DeputeContent,
                           n.Salary,
                           n.ViewCount,
                           Status = n.Status.Name,
                           n.Region.City,
                           n.Provider.FImagePath
                       };

            foreach (var item in data)
            {
                CDeputeViewModel x = CDeputeViewModel.getInstance();
                
                    x.id = item.DeputeId;
                    x.title = item.Title;
                    x.providername = item.Name;
                    x.startdate = item.SrartDate;
                    x.modifieddate = item.Modifiedate;
                    x.deputeContent = item.DeputeContent;
                    x.salary = item.Salary;
                    x.viewcount = item.ViewCount;
                    x.status = item.Status;
                    x.region = item.City;
                    x.imgfilepath = item.FImagePath;

                List.Add(x);
                Temp = List;
            }
        }



    }
}