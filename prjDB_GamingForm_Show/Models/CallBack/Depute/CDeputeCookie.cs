using DB_GamingForm_Show.Job.DeputeClass;
using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeCookie : Controller
    {
        private List<CDeputeViewModel>? CookieList { get; set; }
        private readonly DbGamingFormTestContext _db;
        private DeputeCookie _dataCookie;
        private CDeputeDataLoad _dataLoad;
        public CDeputeCookie
        (
            DbGamingFormTestContext context,
            DeputeCookie dataCookie,
            CDeputeDataLoad dataLoad
        )

        {
            _db = context;
            _dataCookie = dataCookie;
            _dataLoad = dataLoad;

            _dataCookie.setcookie += setCookie;
            _dataCookie.getcookie += getCookie;
            _dataCookie.setfav+= setFav;
            _dataCookie.getfav+= getFav;

        }

        public IActionResult setCookie(CKeyWord vm)
        {
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);

            string record = "";
            if (HttpContext.Request.Cookies[userid.ToString()] != null)
                record = HttpContext.Request.Cookies[userid.ToString()];

            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddDays(30);
            record += $"{vm.txtId},";
            HttpContext.Response.Cookies.Append(userid.ToString(), record, options);//

            return Content("Succeed");
        }
        public IActionResult getCookie(CKeyWord vm)
        {
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            string record = "";
            if (HttpContext.Request.Cookies[userid.ToString()] != null)
                record = HttpContext.Request.Cookies[userid.ToString()];
            string[] strResult = record.Split(',');
            strResult = strResult.Reverse().Distinct().ToArray();
            IEnumerable<CDeputeViewModel> datas = null;
            foreach (var item in strResult)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    datas = from n in _dataLoad.getList(vm) as IEnumerable<CDeputeViewModel>
                            where n.id == Convert.ToInt32(item)
                            select n;
                    foreach (var data in datas)
                    {
                        CookieList.Add(data);
                    }
                }

            }

            return Json(CookieList);

        }
        //加入我的最愛
        public IActionResult setFav(CKeyWord vm)
        {

            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);

            string record = "";
            if (HttpContext.Request.Cookies[$"fav{userid}"] != null)
                record = HttpContext.Request.Cookies[$"fav{userid}"];

            string[] strResult = record.Split(',');
            if (strResult.Contains(vm.txtId.ToString()))
            {
                record = "";
                HttpContext.Response.Cookies.Delete($"fav{userid}");
                var data = strResult.Where(n => n != vm.txtId.ToString());
                foreach (var item in data)
                {
                    if (!string.IsNullOrEmpty(item))
                        record += item.ToString() + ",";
                }
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(30);
                HttpContext.Response.Cookies.Append($"fav{userid}", record, options);////
                return Content("false");
            }
            else
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(30);
                record += $"{vm},";
                HttpContext.Response.Cookies.Append($"fav{userid}", record, options);//

                return Content("true"); ;
            }
        }
        public IActionResult getFav(CKeyWord vm)
        {
            int userid = 0;
            if (HttpContext.Session.GetInt32(CDictionary.SK_UserID) != null)
                userid = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            string record = "";
            if (HttpContext.Request.Cookies[$"fav{userid}"] != null)
                record = HttpContext.Request.Cookies[$"fav{userid}"];
            string[] strResult = record.Split(',');
            strResult = strResult.Reverse().Distinct().ToArray();
            IEnumerable<CDeputeViewModel> datas = null;
            foreach (var item in strResult)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    datas = from n in _dataLoad.getList(vm) as IEnumerable<CDeputeViewModel>
                            where n.id == Convert.ToInt32(item)
                            select n;
                    foreach (var indata in datas)
                    {
                        CookieList.Add(indata);
                    }
                }

            }
            return Json(CookieList.Take(5));
        }
        

    }
}
