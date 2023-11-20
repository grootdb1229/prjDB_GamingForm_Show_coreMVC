using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using prjDB_GamingForm_Show.Models;

namespace prjDB_GamingForm_Show.Controllers
{
    public class AdminUseSuperController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_管理者登入資訊使用關鍵字))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    Controller = "AdminHome",
                    action = "Login",
                }));
            }
        }
    }
}
