using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class RegionDistrict
{
    public int Zipcode { get; set; }

    public int RegionId { get; set; }

    public string District { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Region Region { get; set; } = null!;
}
