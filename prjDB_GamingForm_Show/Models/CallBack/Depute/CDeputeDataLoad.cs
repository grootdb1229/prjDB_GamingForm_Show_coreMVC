using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeDataLoad:Controller
    {

        private readonly DbGamingFormTestContext _db;
        private DeputeDataLoad _dataLoad;
        private CDeputeViewModel _viewModel;
        private List<CDeputeViewModel> _list;

        public CDeputeDataLoad
        (
         DbGamingFormTestContext context,
         DeputeDataLoad dataLoad,
         CDeputeViewModel viewModel,
         List<CDeputeViewModel> list
        )
        {
            //di
            _db = context;
            _dataLoad = dataLoad;
            _viewModel = viewModel;
            _list = list;

            //di event register
            _dataLoad.getlist+= getList;
            _dataLoad.getrecommend+= getRecommand;
            _dataLoad.getdetailskills += getDetailSkills;
        }
        public List<CDeputeViewModel> getList(CKeyWord vm)
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
                           Skillid = listSkill(n.DeputeId),
                           Skillclassid = listSkillClass(n.DeputeId)


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
                _viewModel.listskillid = item.Skillid;
                _viewModel.listskillclassid = item.Skillclassid;

                _list.Add(_viewModel);
            }

            return _list;

        }
        private string listSkill(int deputeId)
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
        private string listSkillClass(int id)
        {
            string result = "";
            _db.Skills.Load();
            _db.SkillClasses.Load();
            var data = (from n in _db.DeputeSkills
                        where n.DeputeId == id
                        select n.Skill.SkillClass.Name).Distinct();

            foreach (var item in data)
            {
                result += item + ",";
            }


            return result;

        }
        public IActionResult getRecommand(CKeyWord vm)
        {
            _db.Skills.Load();
            _db.Deputes.Load();
            _db.Members.Load();
            var value = from n in _db.DeputeSkills.AsEnumerable()
                        where n.DeputeId == vm.txtId
                        select n.Skill.Name;
            List<CDeputeViewModel> Rcolist = new List<CDeputeViewModel>();
            foreach (var item in value)
            {
                var RecoData = (from n in _db.DeputeSkills.AsEnumerable()
                                where n.Skill.Name.Contains(item)
                                select new
                                {
                                    n.DeputeId,
                                    Title = (n.Depute.Title.Length > 15) ? n.Depute.Title.Substring(0, 10) : n.Depute.Title,
                                    DeputeContent = (n.Depute.DeputeContent.Length > 15) ? n.Depute.DeputeContent.Substring(0, 10) : n.Depute.DeputeContent,
                                    n.Depute.Provider.Name,
                                    n.Depute.Provider.FImagePath
                                }).Distinct();
                foreach (var item2 in RecoData)
                {
                    _viewModel.id = item2.DeputeId;
                    _viewModel.title = item2.Title;
                    _viewModel.providername = item2.Name;
                    _viewModel.deputeContent = item2.DeputeContent;
                    _viewModel.imgfilepath = item2.FImagePath;
                    Rcolist.Add(_viewModel);
                }

            }

            Random rnd = new Random();
            int count = rnd.Next(0, Rcolist.Count());
            List<CDeputeViewModel> Rcolist2 = new List<CDeputeViewModel>();
            if (Rcolist.Count() > 0)
            {
                for (int i = 1; i <= 6; i++)
                {
                    if (i <= Rcolist.Count())
                    {
                        Rcolist2.Add(Rcolist[count]);
                        Rcolist.RemoveAt(count);
                        count = rnd.Next(0, Rcolist.Count);
                    }
                }
            }
            return  Json(Rcolist2);


        }
        public IActionResult getDetailSkills(CKeyWord vm)
        {
            if (vm.txtId == null)
                return Content("No result match");
            else
            {
                _db.SkillClasses.Load();
                IEnumerable<string> skillname = (from n in _db.DeputeSkills
                                                 where n.DeputeId == vm.txtId
                                                 select n.Skill.Name).Distinct();
                return Json(skillname);
            }
        }
    }
}
