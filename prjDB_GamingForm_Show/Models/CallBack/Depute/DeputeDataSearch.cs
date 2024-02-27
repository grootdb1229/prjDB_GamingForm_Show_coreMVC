using DB_GamingForm_Show.Job.DeputeClass;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Shop;
using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeDataSearch
    {
        internal event MutiSearch mutisearch;
        public IActionResult mutiSearch(ref CKeyWord vm)
        {
            return mutisearch(vm);
        }
        
    }
}