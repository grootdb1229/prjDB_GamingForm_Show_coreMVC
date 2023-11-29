using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CShopPageViewModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = null!;

        public decimal Price { get; set; }

        public DateTime AvailableDate { get; set; }

        public string? ProductContent { get; set; }

        public int UnitStock { get; set; }

        public int StatusId { get; set; }

        public int? MemberId { get; set; }

        public int? FirmId { get; set; }

        public string? FImagePath { get; set; }

        public string SubTagName { get; set; }

        public virtual Member? Member { get; set; }
        public IEnumerable<CProductWarp> CProductWarp { get; set; } = new List<CProductWarp>();
        public virtual IEnumerable<Product> Product { get; set; } = new List<Product>();
        public virtual IEnumerable<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

        public virtual IEnumerable<ProductAdvertise> ProductAdvertises { get; set; } = new List<ProductAdvertise>();

        public virtual IEnumerable<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

        public virtual IEnumerable<ProductEvaluate> ProductEvaluates { get; set; } = new List<ProductEvaluate>();

        public virtual IEnumerable<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

        public virtual Status Status { get; set; } = null!;

        public virtual IEnumerable<WishList> WishLists { get; set; } = new List<WishList>();
    }
}
