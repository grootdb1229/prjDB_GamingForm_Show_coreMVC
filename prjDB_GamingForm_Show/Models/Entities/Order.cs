using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Order
{
    [DisplayName("編號")]
    public int OrderId { get; set; }
    [DisplayName("會員編號")]
    public int? MemberId { get; set; }
    [DisplayName("優惠券")]
    public int? CouponId { get; set; }
    [DisplayName("訂單日期")]
    public DateTime OrderDate { get; set; }
    [DisplayName("支付日期")]
    public DateTime? PaymentDate { get; set; }
    [DisplayName("完成日期")]
    public DateTime? CompletedDate { get; set; }
    [DisplayName("支付方式")]
    public int PaymentId { get; set; }

    public string? Note { get; set; }
    [DisplayName("訂單狀態")]
    public int StatusId { get; set; }
    [DisplayName("訂單總價")]
    public decimal SumPrice { get; set; }

    public string? Ecid { get; set; }

    public virtual Coupon? Coupon { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual Payment Payment { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
