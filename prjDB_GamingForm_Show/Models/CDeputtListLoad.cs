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
        private CDeputeDataLoad _dataLoad;
        private CDeputeViewModel _viewModel;
        public CDeputtListLoad
            (
            IWebHostEnvironment host,
            DbGamingFormTestContext db,
            CDeputeDataLoad dataLoad,
            CDeputeViewModel viewModel
            )
        {
            _host = host;
            _db = db;
            _dataLoad = dataLoad;
            _viewModel = viewModel;
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
                
                    _viewModel.id = item.DeputeId;
                    _viewModel.title = item.Title;
                    _viewModel.providername = item.Name;
                    _viewModel.startdate = item.SrartDate;
                    _viewModel.modifieddate = item.Modifiedate;
                    _viewModel.deputeContent = item.DeputeContent;
                    _viewModel.salary = item.Salary;
                    _viewModel.viewcount = item.ViewCount;
                    _viewModel.status = item.Status;
                    _viewModel.region = item.City;
                    _viewModel.imgfilepath = item.FImagePath;

                List.Add(_viewModel);
                Temp = List;
            }
        }



    }
}