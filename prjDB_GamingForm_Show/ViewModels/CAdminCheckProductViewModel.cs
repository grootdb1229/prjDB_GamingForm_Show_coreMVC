using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CAdminCheckProductViewModel
    {
        public IEnumerable<Product>? Products { get; set; }
        public IEnumerable<Member>? Members { get; set; }
    }
}
