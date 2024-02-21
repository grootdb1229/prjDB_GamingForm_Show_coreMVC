using DB_GamingForm_Show.Job.DeputeClass;

namespace prjDB_GamingForm_Show.Models.Interface
{
    public class Ddepute
    {
        public delegate List<CDeputeViewModel> DeputeDelegate();
        public delegate string SkillDelegate(int x);

    }
}
