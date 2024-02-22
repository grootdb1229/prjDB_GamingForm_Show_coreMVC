using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeSearch
    {
        private static class LazyHolder
        {

            internal static CDeputeSearch uniqueInstance = new CDeputeSearch();
        }

        private CDeputeSearch() 
        {
        }

        public static CDeputeSearch getInstance()
        {
            return LazyHolder.uniqueInstance;
        }

        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        private readonly DeputeDataSearch _dataSearch;
        private readonly CDeputeDataLoad _dataLoad;
        public List<CDeputeViewModel> Temp { get; set; }

        public CDeputeSearch
        (
            IWebHostEnvironment host, 
            DbGamingFormTestContext context, 
            DeputeDataSearch dataSearch,
            CDeputeDataLoad dataLoad
        )
        {
            _host = host;
            _db = context;
            _dataSearch = dataSearch;
            _dataLoad = dataLoad;
        }

        private List<CDeputeViewModel> selectedSearch(CKeyWord vm)
        {
            Temp = _dataLoad.returnList();
            IEnumerable<CDeputeViewModel> datas = Temp.Where(n =>
                            (DateTime.Now.Date - Convert.ToDateTime(n.modifieddate)).Days <= vm.txtDate &&
                            n.viewcount >= vm.txtView &&
                            n.salary >= vm.txtSalary);
            Temp = datas.ToList();
            return Temp;
        }

        private List<CDeputeViewModel> orderBy(CKeyWord vm)
        {
            Temp = _dataLoad.returnList();
            IEnumerable<CDeputeViewModel> datas = null;
            switch (vm.txtOrderby)
            {
                case 1:
                    datas = Temp.OrderByDescending(n => n.salary);
                    break;
                case 2:
                    datas = Temp.OrderByDescending(n => Convert.ToDateTime(n.modifieddate));
                    break;
                case 3:
                    datas = Temp.OrderByDescending(n => n.viewcount);
                    break;
                default:
                    datas = Temp;
                    break;
            }
            if (vm.txtEsc)
                datas = datas.Reverse();
            Temp = datas.ToList();

            return Temp;
        }

        public List<CDeputeViewModel> returnResult(CKeyWord vm)
        {
            _dataSearch.mutiResult += selectedSearch;
            _dataSearch.mutiResult += orderBy;
            return _dataSearch.mutiSearch(ref vm);
        }

        
    }
}
