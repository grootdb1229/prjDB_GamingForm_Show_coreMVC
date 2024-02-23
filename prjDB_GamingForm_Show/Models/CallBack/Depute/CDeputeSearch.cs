using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeSearch
    {

        //Static Inner Class
        private static class LazyHolder
        {
            internal static CDeputeSearch uniqueInstance = new CDeputeSearch();
        }

        private CDeputeSearch() {}

        public static CDeputeSearch getInstance()
        {
            return LazyHolder.uniqueInstance;
        }
        //DI
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        //Callback
        private readonly DeputeDataSearch _dataSearch;
        
        public CDeputeSearch
        (
            IWebHostEnvironment host, 
            DbGamingFormTestContext context,
            DeputeDataSearch dataSearch
        )
        {
            _host = host;
            _db = context;
            _dataSearch = dataSearch;
        }
        public List<CDeputeViewModel> Temp { get; set; }
        public CDeputeDataLoad dataLoad = CDeputeDataLoad.getInstance();
        private List<CDeputeViewModel> mutipleSearch(CKeyWord vm)
        {
            Temp = dataLoad.returnList();
            IEnumerable<CDeputeViewModel> datas = null;
            if (vm.txtMutiKeywords != null)
            {
                if (vm.txtMutiKeywords[0] != null)
                {
                    foreach (var item in vm.txtMutiKeywords)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            _db.SerachRecords.Add(new SerachRecord { Name = item, CreateDays = (DateTime.Now.Date) });
                            _db.SaveChanges();
                        }

                        datas = Temp.Where(n => (n.deputeContent.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.title.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.listskillclassid.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.listskillid.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.providername.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.region.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.status.Trim().ToLower().Contains(item.Trim().ToLower())
                                                   ))
                                                   .OrderByDescending(n => n.modifieddate);
                        Temp = datas.ToList();

                    }
                }
            }

            selectedSearch(vm);

            return Temp;
        }

        private void selectedSearch(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = Temp.Where(n =>
                            (DateTime.Now.Date - Convert.ToDateTime(n.modifieddate)).Days <= vm.txtDate &&
                            n.viewcount >= vm.txtView &&
                            n.salary >= vm.txtSalary);
            Temp = datas.ToList();
            orderBy(vm);

        }

        private void orderBy(CKeyWord vm)
        {
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

            returnResult(vm);

        }


        public void Move()
        {
            IEnumerable<CDeputeViewModel> datas = null;
            datas = Temp.Skip(10).Take(10);
            Temp = datas.ToList();
        }

        public List<CDeputeViewModel> returnResult(CKeyWord vm)
        {
            _dataSearch.mutiResult += mutipleSearch;
            return _dataSearch.mutiSearch(ref vm);
        }

        
    }
}
