using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class RegionDistrict
{
    public int Zipcode { get; set; }

    public int RegionId { get; set; }

    public string District { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;
}
