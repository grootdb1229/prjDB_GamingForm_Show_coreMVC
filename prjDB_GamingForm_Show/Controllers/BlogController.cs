using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModel;

namespace prjDB_GamingForm_Show.Controllers
{
    public class BlogController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        //public ActionResult List(int? FId)
        //{
        //    DbGamingFormTestContext db = new
        //     DB_GamingFormEntities db = new DB_GamingFormEntities();
        //    CBlogViewModel vm = null;
        //    if (FId == null)
        //    {

        //        vm = new CBlogViewModel
        //        {
        //            tags = db.Tags.Select(p => p),
        //            subTags = db.SubTags.Where(s => s.TagID == 4 && s.SubTagID != 14).Select(p => p),
        //            blogs = db.Blogs.Select(p => p),
        //            subBlogs = db.SubBlogs.Select(p => p),
        //            articles = db.Articles.OrderByDescending(a => a.ModifiedDate).Select(p => p)
        //        };
        //    }
        //    else
        //    {
        //        vm = new CBlogViewModel
        //        {
        //            tags = db.Tags.Select(p => p),
        //            subTags = db.SubTags.Where(s => s.TagID == 4 && s.SubTagID != 14).Select(p => p),
        //            blogs = db.Blogs.Where(b => b.SubTagID == FId).Select(p => p),
        //            subBlogs = db.SubBlogs.Where(s => s.Blog.SubTagID == FId).Select(p => p),
        //            articles = db.Articles.Where(a => a.SubBlog.Blog.SubTagID == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p)
        //        };
        //    }
        //    return View(vm);
        //}
    }
}
