using System.ComponentModel;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CLoginViewModel
    {
        public string txtAccount { get; set; }
        public string txtPassword { get; set; }
        [DisplayName("電子信箱")]
        public string txtConfirm_Email { get; set; }
    }
}
