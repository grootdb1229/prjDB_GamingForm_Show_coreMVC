using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Interface
{
    public class CMainPage
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        private readonly DeputeHome _dh;
        public CMainPage(IWebHostEnvironment host,DbGamingFormTestContext context, DeputeHome home) 
        {
            _host = host;
            _db = context;
            _dh = home;
        }
      
        private List<CDeputeViewModel> listLoad()
        {
            DbGamingFormTestContext _db = new DbGamingFormTestContext();
            List<CDeputeViewModel> Temp = new List<CDeputeViewModel>();
            List<CDeputeViewModel> List = new List<CDeputeViewModel>();
            _db.Members.Load();
            _db.Statuses.Load();
            _db.Regions.Load();
            _db.Deputes.Load();
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
                           n.Provider.FImagePath,
                           Skillid = GetSkill(n.DeputeId),
                           Skillclassid = GetSkillClass(n.DeputeId)


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
                    imgfilepath = item.FImagePath,
                    listskillid = item.Skillid,
                    listskillclassid = item.Skillclassid,

                };

                List.Add(x);

                
            }
            return List;



        }

        private string GetSkill(int deputeId)
        {
            string result = "";
            _db.Skills.Load();
            _db.SkillClasses.Load();
            var data = (from n in _db.DeputeSkills
                        where n.DeputeId == deputeId
                        select n.Skill.Name).Distinct();
            foreach (var item in data)
            {
                result += item + ",";

            }

            return result;
        }

        private string GetSkillClass(int deputeId)
        {
            string result = "";
            _db.Skills.Load();
            _db.SkillClasses.Load();
            var data = (from n in _db.DeputeSkills
                        where n.DeputeId == deputeId
                        select n.Skill.SkillClass.Name).Distinct();

            foreach (var item in data)
            {
                result += item + ",";

            }


            return result;

        }

        public List<CDeputeViewModel> ReturnList()
        {
            _dh.load += this.listLoad;
            return _dh.GetList();
        }

        public string ReturnSkill(int x)
        {
            _dh.sLoad += this.GetSkill;
            return _dh.GetSkill(ref x);
        }

        public string ReturnSkillClass(int x)
        {
            _dh.sLoad += this.GetSkillClass;
            return _dh.GetSkillClass(ref x);
        }
    }
}
