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

        public Entities.Member member {get{ return _member; }  set { this.member = value; } }
    }
}
