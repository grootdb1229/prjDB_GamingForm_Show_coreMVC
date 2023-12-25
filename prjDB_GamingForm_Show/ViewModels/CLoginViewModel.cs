using System.ComponentModel;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CLoginViewModel
    {
        public string txtAccount { get; set; }
        public string txtPassword { get; set; }
        [DisplayName("電子信箱")]
        public string txtConfirm_Email { get; set; }
        [DisplayName("輸入新的會員密碼")]
        public string txtVal_Password { get; set; }
        [DisplayName("請再輸入一次上述密碼")]
        public string Check_txtVal_Password { get; set; }
        [DisplayName("請輸入驗證碼")]
        public int txtVal_Number { get; set; }
    }
}
