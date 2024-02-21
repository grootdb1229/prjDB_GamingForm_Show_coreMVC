using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.Entities;
using static prjDB_GamingForm_Show.Models.Interface.Ddepute;

namespace prjDB_GamingForm_Show.Models.Interface
{
    public class DeputeHome
    {
        internal event DeputeDelegate load;
        internal event SkillDelegate sLoad;


        public List<CDeputeViewModel> GetList()
        {
             return load();
        }

        public string GetSkill(ref int x)
        {
            return sLoad(x);
        }

        public string GetSkillClass(ref int x)
        {
            return sLoad(x);
        }
        public void Start()
        {
            if (load != null)
            {
                load();
            }

            
        }
    }
}
