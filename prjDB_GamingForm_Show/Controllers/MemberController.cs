using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test() 
        {
            return View();
        }
        public IActionResult MemberPage(int? id)
        {
            //if (id == null)
            //    return RedirectToAction("Create");
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            IEnumerable<Member> datas = null;
            datas = from m in db.Members
                    where m.MemberId == id
                    select m;
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost] 
        public IActionResult Create(Member member)
        {
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            db.Members.Add(member);
            db.SaveChanges();
            int id = db.Members.Where(p => p.Email == member.Email).Select(p => p.MemberId).FirstOrDefault();
            return RedirectToAction("MemberPage", new { id });
        }

        public IActionResult Edit(int? id)
        {
            DbGamingFormTestContext db = new DbGamingFormTestContext();
            Member member = db.Members.FirstOrDefault(p => p.MemberId == id);
            if (member == null)
                return RedirectToAction("MemberPage");
            else
                return View(member);
        }
        //[HttpPost]
        //public IActionResult Edit(int? id)
        //{

        //}

    }
}
