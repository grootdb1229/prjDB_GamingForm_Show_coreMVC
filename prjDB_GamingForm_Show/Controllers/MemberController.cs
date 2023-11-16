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
        public async Task<IActionResult> Create([Bind("Name,Phone,Email,Password")] Member member)
        {
            if (ModelState.IsValid) 
            {
                DbGamingFormTestContext db = new DbGamingFormTestContext();
                db.Members.Add(member);
                await db.SaveChangesAsync();
                return RedirectToAction("MemberPage");
            }
            return View();
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
