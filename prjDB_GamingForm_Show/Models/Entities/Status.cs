﻿using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Status
{
    public int StatusId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Advertise> Advertises { get; set; } = new List<Advertise>();

    public virtual ICollection<Coupon> Coupons { get; set; } = new List<Coupon>();

    public virtual ICollection<DeputeComplain> DeputeComplains { get; set; } = new List<DeputeComplain>();

    public virtual ICollection<DeputeRecord> DeputeRecords { get; set; } = new List<DeputeRecord>();

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
