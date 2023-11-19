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
                    blogs = _db.Blogs.Where(b=>b.SubTagId != 14).Include(b=>b.SubBlogs).Select(p => p),
                    subBlogs = _db.SubBlogs.Include(a => a.Articles).Select(p => p),

                    articles = _db.Articles.Where(a=>a.SubBlog.Blog.SubTagId!=14).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    actions = _db.Actions,
                    articleActions = _db.ArticleActions,

                    //artTitle = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId != 14).OrderByDescending(a => a.ModifiedDate).Select(p => p.Title)

                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Where(b =>  b.SubTagId ==FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Include(a => a.Articles).Where(s => s.Blog.SubTagId == FId).Select(p => p),

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
                    articles = _db.Articles.Include(a => a.Member).Where(a => a.SubBlog.BlogId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),

                };
            }
            else
            {
                vm = new CBlogViewModel
                {
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = _db.Articles.Include(a => a.Member).Include(a=>a.Replies).Where(a => a.SubBlogId == SFId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
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
                articles = _db.Articles.Include(a => a.Member).AsEnumerable().Where(a => a.ArticleId == AFId).Select(p => p),
                actions = _db.Actions,
                articleActions = _db.ArticleActions,
                replies = _db.Replies.Include(a=>a.Member).Where(a => a.ArticleId == AFId).ToList(),
                //members = _db.Members.Include(a=>a.Image)
            };

            var artcon = _db.Articles.Where(a => a.ArticleId == AFId).Select(a => a);

            return View(vm);
        }
        public ActionResult ArticleDelete(int? SFId, int? AFId)
        {
            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);
            if (art != null)
            {
                _db.Articles.Remove(art);
                _db.SaveChanges();
            }

            return RedirectToAction("ArticleList", new { SFId });
        }
    }
}
