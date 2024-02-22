using DB_GamingForm_Show.Job.DeputeClass;
using MailKit.Search;
using prjDB_GamingForm_Show.Models.Shop;
using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeDataSearch
    {
        internal event MutiSearch mutiResult;

        public List<CDeputeViewModel> mutiSearch(ref CKeyWord vm)
        {

            return mutiResult(vm);
        }
        
    }
}