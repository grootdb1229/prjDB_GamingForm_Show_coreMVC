using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.Models.Member
{
    public class CMemberWrap
    {
        private Entities.Member _member;

        public CMemberWrap()
        {
            _member = new Entities.Member();
        }

        public Entities.Member member { get { return _member; } set { this.member = value; } }
        public int MemberId { get { return _member.MemberId; } set { this.member.MemberId = value; } }
        public string Name { get { return _member.Name; } set { this.member.Name = value; } }
        public string Phone { get { return _member.Phone; } set { this.member.Phone = value; } }
        public DateTime Birth { get { return _member.Birth; } set { this.member.Birth = value; } }
        public string Email { get { return _member.Email; } set { this.member.Email = value; } }
        public string Password { get { return _member.Password; } set { this.member.Password = value; } }
        public string FImagePath{ get { return _member.FImagePath; } set { this.member.FImagePath = value; } }
        public int Gender { get { return _member.Gender;} set { this.member.Gender = value; } }
        public IFormFile photo {get; set;}
    }
}
