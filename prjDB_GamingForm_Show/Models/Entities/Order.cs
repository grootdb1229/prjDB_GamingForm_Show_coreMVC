using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Order
{
    public int OrderId { get; set; }

    public int? MemberId { get; set; }

    public int? CouponId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? PaymentDate { get; set; }

    public DateTime? CompletedDate { get; set; }

    public int PaymentId { get; set; }

    public string? Note { get; set; }

    public int StatusId { get; set; }

    public decimal SumPrice { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
