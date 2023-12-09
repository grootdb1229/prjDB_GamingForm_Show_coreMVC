using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class CKeyWord

    {
        public string txtKeyword { get; set; }
        public List<string> txtMutiKeywords { get; set; }
        public string txtHotkey { get; set; }
        //public string txtSkill { get; set; }
        public int txtDate { get; set; }
        public int txtView { get; set; }
        public int txtSalary { get; set; }
    }
}