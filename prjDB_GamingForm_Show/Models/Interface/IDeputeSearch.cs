using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.Models.Interface
{
    public interface IDeputeSearch
    {
        IActionResult setCookie(CKeyWord vm);
        IActionResult getCookie(CKeyWord vm);

        bool setFav(int? id);

        IActionResult getFav(CKeyWord vm);
    }
}
