using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack
{
    public class Ddepute
    {
        public delegate List<CDeputeViewModel> DataDelegate();
        public delegate string SkillDelegate(int x);
        public delegate List<CDeputeViewModel>  MutiSearch(CKeyWord vm);

    }
}
