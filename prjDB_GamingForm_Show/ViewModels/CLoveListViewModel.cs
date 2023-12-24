using System.ComponentModel;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CLoveListViewModel
    {
        public int ProductId
        {
            get;
            set;
        }
            [DisplayName("產品名稱")]
        public string ProductName { get; set; } = null!;

        [DisplayName("產品金額")]
        public decimal Price { get; set; }
        public string? FImagePath { get; set; }
        public bool IsPurchased { get; set; }
    }
}
