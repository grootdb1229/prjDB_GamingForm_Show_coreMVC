using System.ComponentModel;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CValidationViewModel
    {
        [DisplayName("會員編號")]
        public int txtVal_MemberID {get;set;}
        [DisplayName("輸入新的會員密碼")]
        public string txtVal_Password { get; set; }
        [DisplayName("請輸入驗證碼")]
        public string txtVal_Code  {get;set;}
    }
}
