using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CProductViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime AvailableDate { get; set; }

        public string? ProductContent { get; set; }

        public int StatusId { get; set; }

        public int ViewCount { get; set; }

        public int? MemberId { get; set; }

        public string? FImagePath { get; set; }

        public int Goods { get; set; }

        public int sells { get; set; }
        public string StatusName { get; set; }

        public virtual Status Status { get; set; } = null!;

        public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
    }
}
