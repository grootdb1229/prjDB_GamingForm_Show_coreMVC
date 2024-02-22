﻿using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.Entities;
using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeDataLoad
    {
        internal event DataDelegate dataLoad;
        internal event SkillDelegate skillLoad;


        public List<CDeputeViewModel> getList()
        {
            return dataLoad();
        }

        public string getSkill(ref int x)
        {
            return skillLoad(x);
        }

        public string getSkillClass(ref int x)
        {
            return skillLoad(x);
        }

    }
}