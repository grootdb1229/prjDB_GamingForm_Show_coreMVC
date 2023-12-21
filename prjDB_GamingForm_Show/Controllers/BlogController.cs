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
                    //tags = _db.Tags.Select(p => p),
                    subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                    blogs = _db.Blogs.Where(b => b.SubTagId != 14 && b.Title.Trim().Contains(kw.txtKeyWord.Trim())).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                    //subBlogs = _db.SubBlogs.Include(a => a.Articles).Select(p => p),
                    //articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId != 14).OrderByDescending(a => a.ModifiedDate).Select(p => p),

                };
            }
            else
            {
                if (FId == null)
                {
                    vm = new CBlogViewModel
                    {
                        //tags = _db.Tags.Select(p => p),
                        subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                        blogs = _db.Blogs.Where(b => b.SubTagId != 14).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                        //subBlogs = _db.SubBlogs.Include(a => a.Articles).Select(p => p),
                        //articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId != 14).OrderByDescending(a => a.ModifiedDate).Include(a => a.SubBlog).Select(p => p),
                    };
                }
                else
                {
                    vm = new CBlogViewModel
                    {
                        //tags = _db.Tags.Select(p => p),
                        subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
                        blogs = _db.Blogs.Where(b => b.SubTagId == FId).Include(b => b.SubBlogs).ThenInclude(s => s.Articles).Select(p => p),
                        //subBlogs = _db.SubBlogs.Include(a => a.Articles).Where(s => s.Blog.SubTagId == FId).Select(p => p),
                        //articles = _db.Articles.Where(a => a.SubBlog.Blog.SubTagId == FId).OrderByDescending(a => a.ModifiedDate).Include(a => a.SubBlog).Select(p => p),
                    };
                }
            }
            ViewBag.SelectedSubTagId = FId;
            return View(vm);
        }

        //-------ArticleList原版------
        //public ActionResult ArticleList(CKeyWordViewModel kw, int? FId, int? SFId)
        //{
        //    CBlogViewModel vm = new CBlogViewModel();
        //    if (!string.IsNullOrEmpty(kw.txtKeyWord))
        //    {
        //        vm = new CBlogViewModel
        //        {
        //            //tags = _db.Tags.Select(p => p),
        //            //subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
        //            blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
        //            subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
        //            articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Where(a => a.SubBlog.BlogId == FId && (a.Title.Contains(kw.txtKeyWord) || a.ArticleContent.Contains(kw.txtKeyWord))).OrderByDescending(a => a.ModifiedDate).Select(p => p),
        //            //replies = _db.Replies.Include(r => r.Member),
        //            members = _db.Members,
        //            subtagTitle=_db.Blogs.Where(b => b.BlogId == FId).Select(_ => _.SubTag.Name).FirstOrDefault()
        //        };
        //    }
        //    else
        //    {
        //        if (FId == null)
        //            return RedirectToAction("List");
        //        if (SFId == null)
        //        {
        //            vm = new CBlogViewModel
        //            {
        //                //tags = _db.Tags.Select(p => p),
        //                //subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
        //                blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
        //                subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
        //                articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Where(a => a.SubBlog.BlogId == FId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
        //                //replies = _db.Replies.Include(r => r.Member),
        //                members = _db.Members,
        //                subtagTitle = _db.Blogs.Where(b=>b.BlogId==FId).Select(_ => _.SubTag.Name).FirstOrDefault(),
        //            };
        //        }
        //        else
        //        {
        //            vm = new CBlogViewModel
        //            {
        //                //tags = _db.Tags.Select(p => p),
        //                //subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
        //                blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
        //                subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
        //                articles = _db.Articles.Include(a => a.Replies).Include(a => a.Member).Where(a => a.SubBlogId == SFId).OrderByDescending(a => a.ModifiedDate).Select(p => p),
        //                //replies = _db.Replies.Include(r => r.Member),
        //                members = _db.Members,
        //                subtagTitle = _db.SubTags.Where(s => s.Blogs.FirstOrDefault().BlogId == FId).Select(s => s.Name).FirstOrDefault()
        //            };
        //        }
        //    }
        //    ViewBag.SelectedSubBlogId = SFId;
        //    return View(vm);
        //}

        //-------ArticleList原版------


        //-------1217測試------

        public ActionResult ArticleList(CKeyWordViewModel kw, int? FId, int? SFId)
        {
            CBlogViewModel vm = new CBlogViewModel();

            if (!string.IsNullOrEmpty(kw.txtKeyWord))
            {
                vm = new CBlogViewModel
                {
                    blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                    articles = _db.Articles
                        .Include(a => a.Replies)
                        .Include(a => a.Member)
                        .Where(a => a.SubBlog.BlogId == FId && (a.Title.Trim().Contains(kw.txtKeyWord.Trim()) || a.ArticleContent.Contains(kw.txtKeyWord)))
                        .OrderByDescending(a => a.IsPinned)  
                        .ThenByDescending(a => a.ModifiedDate)
                        .Select(p => p),
                    members = _db.Members,
                    subtagTitle = _db.Blogs.Where(b => b.BlogId == FId).Select(_ => _.SubTag.Name).FirstOrDefault(),
                };
            }
            else
            {
                if (FId == null)
                {
                    return RedirectToAction("List");
                }

                if (SFId == null)
                {
                    vm = new CBlogViewModel
                    {
                        blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                        subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                        articles = _db.Articles
                            .Include(a => a.Replies)
                            .Include(a => a.Member)
                            .Where(a => a.SubBlog.BlogId == FId)
                            .OrderByDescending(a => a.IsPinned)  
                            .ThenByDescending(a => a.ModifiedDate)
                            .Select(p => p),
                        members = _db.Members,
                        subtagTitle = _db.Blogs.Where(b => b.BlogId == FId).Select(_ => _.SubTag.Name).FirstOrDefault(),
                    };
                }
                else
                {
                    vm = new CBlogViewModel
                    {
                        blogs = _db.Blogs.Where(b => b.BlogId == FId).Select(p => p),
                        subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId).Select(p => p),
                        articles = _db.Articles
                            .Include(a => a.Replies)
                            .Include(a => a.Member)
                            .Where(a => a.SubBlogId == SFId)
                            .OrderByDescending(a => a.IsPinned)  // 先顯示置頂的文章
                            .ThenByDescending(a => a.ModifiedDate)
                            .Select(p => p),
                        members = _db.Members,
                        subtagTitle = _db.SubTags.Where(s => s.Blogs.FirstOrDefault().BlogId == FId).Select(s => s.Name).FirstOrDefault(),
                    };
                }
            }

            ViewBag.SelectedSubBlogId = SFId;
            return View(vm);
        }



        //-------1217測試------


        public ActionResult ArticleContent(int? FId, int? AFId)            
        {
            //Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);
            //if (art != null)
            //{
            //    art.ViewCount++;
            //    _db.SaveChanges();
            //}
            //CBlogViewModel vm = new CBlogViewModel();
            //vm = new CBlogViewModel
            //{
            //    //tags = _db.Tags.Select(p => p),
            //    //subTags = _db.SubTags.Where(s => s.TagId == 4 && s.SubTagId != 14).Select(p => p),
            //    blogs = _db.Blogs.Include(p => p.SubBlogs).Where(b => b.BlogId == FId),
            //    subBlogs = _db.SubBlogs.Where(s => s.BlogId == FId)/*.Include(s => s.Articles)*/,
            //    articles = _db.Articles.Include(a => a.Member).AsEnumerable().Where(a => a.ArticleId == AFId),
            //    subtagTitle = _db.Blogs.Where(b => b.BlogId == FId).Select(_ => _.SubTag.Name).FirstOrDefault(),
            //    //actions = _db.Actions,
            //    articleActions = _db.ArticleActions.Where(a => a.ArticleId == AFId),
            //    replies = _db.Replies.Include(a => a.Member).Where(a => a.ArticleId == AFId).ToList(),
            //    //members = _db.Members,
            //    ComplainssubTags = _db.SubTags.Where(s => s.TagId == 6)
            //};
            //var artcon = _db.Articles.Where(a => a.ArticleId == AFId).Select(a => a);
            //ViewBag.SelectedSubBlogId = artcon.First().SubBlogId;

            //return View(vm);

            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);
            if (art != null)
            {
                art.ViewCount++;
                _db.SaveChanges();
            }

            CBlogViewModel vm = new CBlogViewModel();
            vm.blogs = _db.Blogs.Include(p => p.SubBlogs).Where(b => b.BlogId == FId);
            _db.SubBlogs.Where(s => s.BlogId == FId).Load(); // 使用 Load 方法進行延遲載入
            vm.subBlogs = _db.SubBlogs.Local; // 從本地集合中獲取載入的 SubBlogs
            _db.Articles.Include(a => a.Member).Where(a => a.ArticleId == AFId).Load(); // 使用 Load 方法進行延遲載入
            vm.articles = _db.Articles.Local; // 從本地集合中獲取載入的 Articles
            vm.subtagTitle = _db.Blogs.Where(b => b.BlogId == FId).Select(_ => _.SubTag.Name).FirstOrDefault();
            _db.ArticleActions.Where(a => a.ArticleId == AFId).Load(); // 使用 Load 方法進行延遲載入
            vm.articleActions = _db.ArticleActions.Local; // 從本地集合中獲取載入的 ArticleActions
            vm.replies = _db.Replies.Include(a => a.Member).Where(a => a.ArticleId == AFId).ToList();
            _db.SubTags.Where(s => s.TagId == 6).Load(); // 使用 Load 方法進行延遲載入
            vm.ComplainssubTags = _db.SubTags.Local; // 從本地集合中獲取載入的 SubTags

            var artcon = _db.Articles.Where(a => a.ArticleId == AFId).Select(a => a);
            ViewBag.SelectedSubBlogId = artcon.First().SubBlogId;

            return View(vm);
        }


        public ActionResult ArticleDelete(int? FId, int? AFId)
        {
            Article art = _db.Articles.FirstOrDefault(a => a.ArticleId == AFId);

            if (art != null)
            {
                // 修改 Article 的 SubBlogID
                art.SubBlogId = 191;  // 新的 SubBlogID
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
            Member member = _db.Members.FirstOrDefault(m => m.MemberId == Inart.MemberId);
            member.BonusPoint += 50;
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
            Member member = _db.Members.FirstOrDefault(m => m.MemberId == Inart.MemberId);
            member.BonusPoint += 10;
            _db.SaveChanges();
            return RedirectToAction("ArticleContent", new { AFId, FId });
        }


        //----------partialview---------------
        public IActionResult shoppartialview()
        {
            Random rm = new Random();

            _db.Products.Load();
            var mo = _db.Products.OrderByDescending(p => p.ViewCount).Select(p => p).Take(5);
            int ram = rm.Next(0, mo.Count());


            return Json(mo.ToList()[ram]);
        }
        //------------------檢舉------------
        [HttpPost]
        public IActionResult ArticleComplain(ArticleComplain inCom)
        {
            _db.ArticleComplains.Add(inCom);
            _db.SaveChanges();

            return Content("檢舉成功");
        }
        public IActionResult HotBlog()
        {
            var hb = _db.Blogs.Where(b => b.SubTagId != 14).OrderByDescending(b=>b.SubBlogs.SelectMany(s=>s.Articles).Sum(a=>a.ViewCount)).Take(5)
                .Select(b => new
            {
                b.Title,
                b.BlogId,
                ArticleViewCount = b.SubBlogs.SelectMany(sb => sb.Articles).Sum(a => a.ViewCount)
            }); ;
            
            return Json(hb);
        }
        public IActionResult HotArticle(int blogid)
        {
            int num = 5;
            int anum = _db.Articles.Where(a => a.SubBlog.BlogId == blogid).Count();
            if(anum<num)
                num= anum;
            var ha = _db.Articles.Where(a => a.SubBlog.BlogId == blogid).OrderByDescending(a => a.ViewCount).Take(num)
                .Select(a => new
                {
                    a.Title,
                    blogid=a.SubBlog.BlogId,
                    a.ArticleId,
                    subtitle=a.SubBlog.Title
                }
                );;

            return Json(ha);
        }

    }
}
