using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class CDeputeCookie
    {

        private static class LazyHolder
        {
            internal static CDeputeCookie uniqueInstance = new CDeputeCookie();
        }
        private CDeputeCookie() { }
        public static CDeputeCookie getInstance()
        {
            return LazyHolder.uniqueInstance;
        }



        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public CDeputeCookie(IWebHostEnvironment host, DbGamingFormTestContext context, DeputeDataLoad dataLoad)
        {
            _host = host;
            _db = context;
        }




    }
}
