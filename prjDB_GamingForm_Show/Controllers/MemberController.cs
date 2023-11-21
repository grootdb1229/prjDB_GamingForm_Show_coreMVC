using Microsoft.AspNetCore.Mvc;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Member;

namespace prjDB_GamingForm_Show.Controllers
{
    public class MemberController : Controller
    {
        private readonly IWebHostEnvironment _host;
        private readonly DbGamingFormTestContext _db;
        public MemberController(IWebHostEnvironment host, DbGamingFormTestContext db)
        {
            _host = host;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Test() 
        //{
        //    return View();
        //}
        public IActionResult MemberPage(int? id)
        {
            //if (id == null)
            //    return RedirectToAction("Create");
            IEnumerable<Member> datas = null;
            datas = from m in _db.Members
                    where m.MemberId == id
                    select m;
            return View(datas);
        }

        public IActionResult Test(int? id) 
        {
            IEnumerable<Member> datas = null;
            datas = from m in _db.Members
                    where m.MemberId == id
                    select m;
            return View(datas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CMemberWrap memberWrap)
        {
            Member member = new Member();
            member.Name = memberWrap.Name;
            member.Phone = memberWrap.Phone;
            member.Birth = memberWrap.Birth;
            member.Email = memberWrap.Email;
            member.Password = memberWrap.Password;
            member.Gender = memberWrap.Gender;
            string photoName = Guid.NewGuid().ToString() + ".jpg";
            member.FImagePath = photoName; 
            memberWrap.photo.CopyTo(new FileStream(_host.WebRootPath + "/MemberPhoto/" + photoName, FileMode.Create));
            _db.Members.Add(member);
            _db.SaveChanges();
            ViewBag.ID = member.MemberId;
            return RedirectToAction("HomePage" , "Home");
        }

        public IActionResult Edit(int? id)
        {
            
            Member member = _db.Members.FirstOrDefault(p => p.MemberId == id);
            if (member == null)
                return RedirectToAction("HomePage", "Home");
            else
                return View(member);
        }
        [HttpPost]
        public IActionResult Edit(Member member)
        {
            Member dbmember = _db.Members.FirstOrDefault(m => m.MemberId == member.MemberId);
            if (member == null) 
            {
                return RedirectToAction("Create");
            }
            //dbmember == null;
            if (dbmember != null)
            {
                dbmember.Name  = member.Name;
                dbmember.Phone = member.Phone;
                dbmember.Email = member.Email;
                dbmember.Password = member.Password;
                _db.SaveChanges();
            }
            return RedirectToAction("HomePage", "Home");
        }


    }
}
