using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System.Security.Cryptography;

namespace prjDB_GamingForm_Show.Controllers
{
    public class BlogController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public BlogController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult List(int? FId)
        {
            
            _db.Actions.Load();
            _db.SubBlogs.Load();
            _db.Blogs.Load();
            _db.Tags.Load();
            _db.SubTags.Load();
            _db.ArticleActions.Load();
            _db.Articles.Load();
            CBlogViewModel vm = new CBlogViewModel();
            if (FId == null)
            {
                vm = new CBlogViewModel
                {
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Select(p => p),
                    subBlogs = _db.SubBlogs.Select(p => p),
                    articles = _db.Articles.AsEnumerable().OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    actions= _db.Actions,
                    articleActions= _db.ArticleActions
                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Where(b => b.SubTagId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.Blog.SubTagId == FId).Select(p => p),
                    articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    actions = _db.Actions,
                    articleActions = _db.ArticleActions
                };
            }
            
            
            return View(vm);
        }
        public ActionResult ArticleList(int? FId, int? SFId)
        {
            
            CBlogViewModel vm = new CBlogViewModel();
            if (FId == null)
                return RedirectToAction("List");
            if (SFId == null)
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = _db.Articles.Where(a => a.SubBlog.BlogId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),

                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),

                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = _db.Articles.Where(a => a.SubBlogId == SFId).OrderByDescending(a => a.ModifiedDate).Select(p => p),

                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                };
            }
            return View(vm);
        }

        public ActionResult ArticleContent(int? AFId)
        {

            CBlogViewModel vm = new CBlogViewModel();
            vm = new CBlogViewModel
            {
                tags = _db.Tags.Select(p => p),
                subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                blogs = _db.Blogs.Select(p => p),
                subBlogs = _db.SubBlogs.Select(p => p),
                articles = _db.Articles.AsEnumerable().OrderByDescending(a => a.ModifiedDate).Select(p => p),
                actions = _db.Actions,
                articleActions = _db.ArticleActions
            };


            var artcon = _db.Articles.Where(a => a.ArticleId== AFId)
                .Select(a => a);

            return View(artcon);
        }
    }
}
