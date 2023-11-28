using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Coupon
{
    public int CouponId { get; set; }

    public string Title { get; set; } = null!;

    public string? Discount { get; set; }

    public string? Reduce { get; set; }

    public string StartDate { get; set; } = null!;

    public string EndDate { get; set; } = null!;

    public int StatusId { get; set; }

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual Status Status { get; set; } = null!;
}
