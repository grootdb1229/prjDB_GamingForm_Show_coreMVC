using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack
{
    public class Ddepute
    {
        public delegate IActionResult DataDelegate(CKeyWord vm);
        public delegate IActionResult MutiSearch(CKeyWord vm);
        public delegate IActionResult CookieDelegate(CKeyWord vm);
        public delegate IActionResult SkillDelegate(CKeyWord vm);

    }
}
