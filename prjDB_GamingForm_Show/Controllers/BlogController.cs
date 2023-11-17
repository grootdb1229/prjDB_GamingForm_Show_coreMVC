using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;

namespace prjDB_GamingForm_Show.Controllers
{
    public class BlogController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? FId)
        {
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            db.Actions.Load();
            CBlogViewModel vm = new CBlogViewModel();
            if (FId == null)
            {
                vm = new CBlogViewModel
                {
                    tags = db.Tags.Select(p => p),
                    subTags = db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = db.Blogs.Select(p => p),
                    subBlogs = db.SubBlogs.Select(p => p),
                    articles = db.Articles.AsEnumerable().OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    actions=db.Actions,
                    articleActions=db.ArticleActions
                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    tags = db.Tags.Select(p => p),
                    subTags = db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = db.Blogs.Where(b => b.SubTagId == FId).Select(p => p),
                    subBlogs = db.SubBlogs.Where(s => s.Blog.SubTagId == FId).Select(p => p),
                    articles = db.Articles.Where(a => a.SubBlog.Blog.SubTagId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    actions = db.Actions,
                    articleActions = db.ArticleActions
                };
            }
            
            return View(vm);
        }
        public ActionResult ArticleList(int? FId, int? SFId)
        {
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            CBlogViewModel vm = new CBlogViewModel();
            if (FId == null)
                return RedirectToAction("List");
            if (SFId == null)
            {
                vm = new CBlogViewModel
                {
                    blogs = db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = db.Articles.Where(a => a.SubBlog.BlogId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p)
                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = db.Articles.Where(a => a.SubBlogId == SFId).OrderByDescending(a => a.ModifiedDate).Select(p => p)
                };
            }
            return View(vm);
        }

        public ActionResult ArticleContent(int? FId)
        {

            DbGamingFormTestContext db = new DbGamingFormTestContext();
            var artcon = db.Articles.Where(a => a.ArticleId== FId)
                .Select(a => a);

            return View(artcon);
        }
    }
}
