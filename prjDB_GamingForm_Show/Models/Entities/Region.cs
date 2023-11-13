using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Region
{
    public int RegionId { get; set; }

    public string City { get; set; } = null!;

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();

    public virtual ICollection<RegionDistrict> RegionDistricts { get; set; } = new List<RegionDistrict>();
}
