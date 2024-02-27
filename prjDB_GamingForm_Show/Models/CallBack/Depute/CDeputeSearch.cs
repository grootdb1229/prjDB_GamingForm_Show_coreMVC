using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeSearch:Controller
    {
        //DI
        private readonly DbGamingFormTestContext _db;
        //Callback
        private DeputeDataSearch _dataSearch;
        private CDeputeDataLoad _dataLoad;
        private List<CDeputeViewModel> _temp;
        public CDeputeSearch
        (
            DbGamingFormTestContext context,
            DeputeDataSearch dataSearch,
            List<CDeputeViewModel> temp
        )
        {
            _db = context;
            _dataSearch = dataSearch;
            _dataSearch.mutisearch += getMutiSearch;
            _temp = temp;
        }

        public IActionResult getMutiSearch(CKeyWord vm)
        {
            _temp = _dataLoad.getList(vm) as List<CDeputeViewModel>;
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

                        datas = _temp.Where(n => (n.deputeContent.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.title.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.listskillclassid.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.listskillid.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.providername.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.region.Trim().ToLower().Contains(item.Trim().ToLower()) ||
                                                   n.status.Trim().ToLower().Contains(item.Trim().ToLower())
                                                   ))
                                                   .OrderByDescending(n => n.modifieddate);
                        _temp = datas.ToList();

                    }
                }
            }

            selectedSearch(vm);

            return Json(_temp);
        }
        private void selectedSearch(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = _temp.Where(n =>
                            (DateTime.Now.Date - Convert.ToDateTime(n.modifieddate)).Days <= vm.txtDate &&
                            n.viewcount >= vm.txtView &&
                            n.salary >= vm.txtSalary);
            _temp = datas.ToList();
            orderBy(vm);

        }
        private void orderBy(CKeyWord vm)
        {
            IEnumerable<CDeputeViewModel> datas = null;
            switch (vm.txtOrderby)
            {
                case 1:
                    datas = _temp.OrderByDescending(n => n.salary);
                    break;
                case 2:
                    datas = _temp.OrderByDescending(n => Convert.ToDateTime(n.modifieddate));
                    break;
                case 3:
                    datas = _temp.OrderByDescending(n => n.viewcount);
                    break;
                default:
                    datas = _temp;
                    break;
            }
            if (vm.txtEsc)
                
                datas = datas.Reverse();
            _temp = datas.ToList();

        }
        public void Move()
        {
            IEnumerable<CDeputeViewModel> datas = null;
            datas = _temp.Skip(10).Take(10);
            _temp = datas.ToList();
        }

        
    }
}
