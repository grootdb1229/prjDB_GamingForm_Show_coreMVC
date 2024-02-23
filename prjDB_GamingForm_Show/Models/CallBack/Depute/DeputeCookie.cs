using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeCookie
    {

        internal event CookieDelegate setcookie;
        internal event CookieDelegate getcookie;

        public void setCookie(int? id)
        {
            setcookie(id);
        }
        public void getCookie(int? id)
        {
            getcookie(id);
        }


    }
}
