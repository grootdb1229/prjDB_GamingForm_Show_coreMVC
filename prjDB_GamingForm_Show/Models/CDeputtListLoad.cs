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
    public class CDeputtListLoad
    {
        public List<CDeputeViewModel> List { get; set; }
        public List<CDeputeViewModel> Temp { get; set; }

        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public CDeputtListLoad(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
            ListLoad();
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



    }
}