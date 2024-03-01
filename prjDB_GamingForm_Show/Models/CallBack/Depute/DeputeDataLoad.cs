using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Shop;
using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeDataLoad:Controller
    {
        internal event ListDelegate getlist;
        internal event DataDelegate getrecommend;
        internal event SkillDelegate getdetailskills;
        public List<CDeputeViewModel> getList(CKeyWord vm)
        {
            return getlist(vm);  
        }

        public IActionResult getRecommand(CKeyWord vm)
        {
            return getrecommend(vm);
        }

        public IActionResult getSkill(CKeyWord vm)
        {
            return getdetailskills(vm);
        }


    }
}
