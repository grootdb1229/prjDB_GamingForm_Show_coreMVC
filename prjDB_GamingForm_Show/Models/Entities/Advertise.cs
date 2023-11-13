using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Advertise
{
    public int AdvertiseId { get; set; }

    public string Title { get; set; } = null!;

    public string AdContent { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<JobAdvertise> JobAdvertises { get; set; } = new List<JobAdvertise>();

    public virtual ICollection<ProductAdvertise> ProductAdvertises { get; set; } = new List<ProductAdvertise>();

    public virtual Status Status { get; set; } = null!;
}
