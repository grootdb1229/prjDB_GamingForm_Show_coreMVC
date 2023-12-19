using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.Models.Shop;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class COrderViewModel
    {
        public int OrderId { get; set; }

        public string? CouponTitle { get; set; }

        public DateTime OrderDate { get; set; }


        public string PaymentName { get; set; }
        public List<string> ProductName { get; set; }

        public List<decimal> Price { get; set; }

        public double Sumprice { get; set; }

        public string MemberName { get; set; }
        public List<CProductNamePrice> products { get; set; }

        public virtual Payment Payment { get; set; } = null!;

        public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
        public virtual Coupon? Coupon { get; set; }


    }
}
