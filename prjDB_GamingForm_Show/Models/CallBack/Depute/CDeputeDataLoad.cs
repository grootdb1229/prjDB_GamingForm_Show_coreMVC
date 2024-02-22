using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeDataLoad
    {
        private static class LazyHolder
        {
            internal static CDeputeDataLoad uniqueInstance = new CDeputeDataLoad();
        }
        private CDeputeDataLoad()
        {
        }
        public static CDeputeDataLoad getInstance()
        {
            return LazyHolder.uniqueInstance;
        }



        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        private readonly DeputeDataLoad _dataLoad;
        public CDeputeDataLoad(IWebHostEnvironment host, DbGamingFormTestContext context, DeputeDataLoad dataLoad)
        {
            _host = host;
            _db = context;
            _dataLoad = dataLoad;
        }
         
        private static List<CDeputeViewModel> List {get; set;}

        private List<CDeputeViewModel> listLoad()
        {
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
                           n.Provider.Name,
                           SrartDate = n.StartDate.ToString("d"),
                           Modifiedate = n.Modifiedate.ToString("d"),
                           n.DeputeContent,
                           n.Salary,
                           n.ViewCount,
                           Status = n.Status.Name,
                           n.Region.City,
                           n.Provider.FImagePath,
                           Skillid = getSkill(n.DeputeId),
                           Skillclassid = getSkillClass(n.DeputeId)


                       };
            CDeputeViewModel x = CDeputeViewModel.getInstance();

            foreach (var item in data)
            {
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
                x.listskillid = item.Skillid;
                x.listskillclassid = item.Skillclassid;


                List.Add(x);


            }
            return List;



        }

        private string getSkill(int deputeId)
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

        private string getSkillClass(int deputeId)
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


        public List<CDeputeViewModel> returnList()
        {
            _dataLoad.dataLoad += listLoad;
            return _dataLoad.getList();
        }

        public string returnSkill(int x)
        {
            _dataLoad.skillLoad += getSkill;
            return _dataLoad.getSkill(ref x);
        }

        public string returnSkillClass(int x)
        {
            _dataLoad.skillLoad += getSkillClass;
            return _dataLoad.getSkillClass(ref x);
        }
    }
}
