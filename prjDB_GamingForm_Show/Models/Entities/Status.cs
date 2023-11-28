using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Status
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Advertise> Advertises { get; set; } = new List<Advertise>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<DeputeRecord> DeputeRecords { get; set; } = new List<DeputeRecord>();

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();

    public virtual ICollection<MemberStatus> MemberStatuses { get; set; } = new List<MemberStatus>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
