using Azure.Identity;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using prjDB_GamingForm_Show.Models;
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

        public ActionResult List(CKeyWordViewModel kw, int? FId)
        {
            CBlogViewModel vm = new CBlogViewModel();

            ViewBag.KK = HttpContext.Session.GetInt32("user_id");
            if (!string.IsNullOrEmpty(kw.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Where(b => b.SubTagId != 14 && b.Title.Contains(kw.txtKeyWord)).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                    subBlogs = _db.SubBlogs.Include(a => a.Articles).Select(p => p),
                    articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId != 14).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                };
            }
            else
            {
                if (FId == null)
                {
                    vm = new CBlogViewModel
                    {
                        tags = _db.Tags.Select(p => p),
                        subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                        blogs = _db.Blogs.Where(b => b.SubTagId != 14).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                        subBlogs = _db.SubBlogs.Include(a => a.Articles).Select(p => p),
                        articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId != 14).OrderByDescending(a => a.ModifiedDate).Include(a => a.SubBlog).Select(p => p),
                    };
                }
                else
                {
                    vm = new CBlogViewModel
                    {
                        tags = _db.Tags.Select(p => p),
                        subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                        blogs = _db.Blogs.Where(b => b.SubTagId == FId).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                        subBlogs = _db.SubBlogs.Include(a => a.Articles).Where(s => s.Blog.SubTagId == FId).Select(p => p),
                        articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId == FId).OrderByDescending(a => a.ModifiedDate).Include(a => a.SubBlog).Select(p => p),
                    };
                }
            }

            return View(vm);
        }
        public ActionResult ArticleList(CKeyWordViewModel kw, int? FId, int? SFId)
        {
            CBlogViewModel vm = new CBlogViewModel();
            if (!string.IsNullOrEmpty(kw.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Where(a => a.SubBlog.BlogId == FId && a.Title.Contains(kw.txtKeyWord)).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                };
            }
            else
            {
                if (FId == null)
                    return RedirectToAction("List");
                if (SFId == null)
                {
                    vm = new CBlogViewModel
                    {
                        blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                        subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                        articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Where(a => a.SubBlog.BlogId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
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
                        articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Include(a => a.Replies).Where(a => a.SubBlogId == SFId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
                    };
                }
            }
            return View(vm);
        }

        public ActionResult ArticleContent(int? FId, int? AFId)
        {
            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);
            if (art != null)
            {
                art.ViewCount++;
                _db.SaveChanges();
            }
            CBlogViewModel vm = new CBlogViewModel();
            vm = new CBlogViewModel
            {
                tags = _db.Tags.Select(p => p),
                subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                blogs = _db.Blogs.Include(p => p.SubBlogs).Where(b => b.BlogId == FId).Select(p => p),
                subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Include(s => s.Articles).Select(p => p),
                articles = _db.Articles.Include(a => a.Member).AsEnumerable().Where(a => a.ArticleId == AFId).Select(p => p),
                actions = _db.Actions,
                articleActions = _db.ArticleActions.Where(a => a.ArticleId == AFId).Select(p => p),
                replies = _db.Replies.Include(a => a.Member).Where(a => a.ArticleId == AFId).ToList(),
                members = _db.Members
            };
            var artcon = _db.Articles.Where(a => a.ArticleId == AFId).Select(a => a);

            return View(vm);
        }


        public ActionResult ArticleDelete(int? FId, int? AFId)
        {
            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);

            if (art != null)
            {
                // 修改 Article 的 SubBlogID
                art.SubBlogId = 42;  // 新的 SubBlogID
                _db.SaveChanges();
            }
            return RedirectToAction("ArticleList", new { FId });
        }





        public ActionResult ReplyDelete(int? RFId, int? AFId, int? FId)
        {
            Reply re = _db.Replies.FirstOrDefault(a => a.ReplyId == RFId);
            if (re != null)
            {
                _db.Replies.Remove(re);
                _db.SaveChanges();
            }
            return RedirectToAction("ArticleContent", new { AFId, FId });
        }


        public IActionResult ArticleCreate(int? FId)
        {

            CBlogViewModel vm = null;
            if (FId == null)
                return RedirectToAction("ArticleList");
            vm = new CBlogViewModel
            {
                blogs = _db.Blogs.Include(b => b.SubBlogs).Where(p => p.BlogId == FId),
                subBlogs = _db.SubBlogs.Include(s => s.Blog).Where(p => p.BlogId == FId).Select(p => p),
                articles = _db.Articles,
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult ArticleCreate(Article Inart, int? FId)
        {
            _db.Articles.Add(Inart);
            _db.SaveChanges();
            return RedirectToAction("ArticleList", new { FId });
        }

        public IActionResult ArticleEdit(int? FId, int? AFId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                blogs = _db.Blogs.Include(b => b.SubBlogs).Where(p => p.BlogId == FId),
                subBlogs = _db.SubBlogs.Include(s => s.Blog).Where(p => p.BlogId == FId).Select(p => p),
                articles = _db.Articles.Where(a => a.ArticleId == (int)AFId).Select(a => a)

            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ArticleEdit(Article Inart, int? FId, int? AFId)
        {
            Article dbArt = _db.Articles.First(a => a.ArticleId == Inart.ArticleId);
            if (dbArt != null)
            {
                dbArt.Title = Inart.Title;
                dbArt.ArticleContent = Inart.ArticleContent;
                dbArt.ModifiedDate = Inart.ModifiedDate;
                dbArt.SubBlogId = Inart.SubBlogId;
            }
            _db.SaveChanges();
            return RedirectToAction("ArticleContent", new { AFId, FId });
        }

 
        public IActionResult ReplyEdit(int? AFId, int? RFId, int? FId)
        {
            CBlogViewModel vm = new CBlogViewModel()
            {
                blogs = _db.Blogs.Include(b => b.SubBlogs).Where(p => p.BlogId == FId),
                subBlogs = _db.SubBlogs.Include(s => s.Blog).Where(p => p.BlogId == FId).Select(p => p),
                articles = _db.Articles.Where(a => a.ArticleId == (int)AFId).Select(a => a),
                replies = _db.Replies.Where(a => a.ReplyId == (int)RFId).Select(a => a),

            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult ReplyEdit(Reply Inrep, int? AFId, int? FId, int? RFId)
        {
            Reply dbArt = _db.Replies.First(a => a.ReplyId == RFId);
            if (dbArt != null)
            {
                dbArt.ReplyContents = Inrep.ReplyContents;
                dbArt.ModifiedDate = Inrep.ModifiedDate;
            }
            _db.SaveChanges();
            return RedirectToAction("ArticleContent", new { AFId, FId });
        }

        public IActionResult Like(int? AFId)
        {
            int memberId = (int)HttpContext.Session.GetInt32(CDictionary.SK_UserID);
            var art = _db.ArticleActions.Where(a => a.ArticleId == AFId && a.MemberId == memberId && a.ActionId == 1).Select(a => a).FirstOrDefault();
            if (art != null)
            {
                _db.ArticleActions.Remove(art);
            }
            else
            {
                ArticleAction x = new ArticleAction();
                x.ArticleId = (int)AFId;
                x.MemberId = memberId;
                x.ActionId = 1;
                _db.ArticleActions.Add(x);
            }
            _db.SaveChanges();
            var likeCount = _db.ArticleActions.Count(a => a.ArticleId == AFId && a.ActionId == 1);
            return Content(likeCount.ToString());
        }


        public IActionResult ReplyCreate(int? AFId, int? FId)
        {
            //if (HttpContext.Session.GetInt32("user_id") == null)
            //    return RedirectToAction("Login", "Home");
            //int memberId = (int)HttpContext.Session.GetInt32("user_id");

            //if (HttpContext.Session.GetInt32("user_id") == null)
            //{
            //    // 如果未登入，將原始頁面的 URL 存儲在 Session 中
            //    HttpContext.Session.SetString("returnUrl", Url.Action("ReplyCreate", "Blog", new { AFId, FId }));
            //    return RedirectToAction("Login", "Home");
            //}

            //ViewBag.KK = HttpContext.Session.GetInt32("user_id");
            CBlogViewModel vm = null;
            if (AFId == null)
                return RedirectToAction("ArticleList");
            vm = new CBlogViewModel
            {
                blogs = _db.Blogs.Where(b => b.BlogId == FId),
                articles = _db.Articles.Include(s => s.Replies).Where(a => a.ArticleId == AFId).Select(p => p),
                replies = _db.Replies
            };

            return View(vm);
        }
        [HttpPost]
        public IActionResult ReplyCreate(Reply Inart, int? AFId, int? FId)
        {
            _db.Replies.Add(Inart);
            _db.SaveChanges();
            return RedirectToAction("ArticleContent", new { AFId, FId });
        }


        //----------partialview---------------
        public IActionResult shoppartialview()
        {
            Random rm = new Random();

            _db.Products.Load();
            int ram = rm.Next(0, _db.Products.Count());
            var mo = _db.Products.Select(p => p);


            return Json(mo.ToList()[ram]);
        }



    }
}
