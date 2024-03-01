using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Shop;
using static prjDB_GamingForm_Show.Models.CallBack.Ddepute;

namespace prjDB_GamingForm_Show.Models.CallBack.Depute
{
    public class DeputeCookie:Controller
    {

        internal event CookieDelegate setcookie;
        internal event CookieDelegate getcookie;
        internal event CookieDelegate setfav;
        internal event DataDelegate getfav;
        

        public IActionResult setCookie(CKeyWord vm)
        {
            return (setcookie(vm));
        }
        public IActionResult getCookie(CKeyWord vm)
        {
            return Json(getcookie(vm));
        }

        public IActionResult setFav(CKeyWord vm)
        {
            return setfav(vm);
        }
        public IActionResult getFav(CKeyWord vm)
        {
            return Json(getfav(vm));
        }
    }
}
