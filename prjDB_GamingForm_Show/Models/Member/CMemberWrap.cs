using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel;

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
        [DisplayName("會員名稱")]
        public string Name { get { return _member.Name; } set { this.member.Name = value; } }
        [DisplayName("會員手機")]
        public string Phone { get { return _member.Phone; } set { this.member.Phone = value; } }
        [DisplayName("生日")]
        public DateTime Birth { get { return _member.Birth; } set { this.member.Birth = value; } }
        [DisplayName("電子信箱")]
        public string Email { get { return _member.Email; } set { this.member.Email = value; } }
        [DisplayName("密碼")]
        public string Password { get { return _member.Password; } set { this.member.Password = value; } }
        public string FImagePath{ get { return _member.FImagePath; } set { this.member.FImagePath = value; } }
        [DisplayName("自我介紹")]
        public string MyComment { get { return _member.Mycomment; } set { this.member.Mycomment = value; } }
        [DisplayName("性別")]
        public int Gender { get { return _member.Gender;} set { this.member.Gender = value; } }
        public IFormFile photo { get; set; }
        public string EmailValMsg { get; set; }
        public string PhoneValMsg { get; set; }
        public string PasswordValMsg { get; set; }

    }
}
