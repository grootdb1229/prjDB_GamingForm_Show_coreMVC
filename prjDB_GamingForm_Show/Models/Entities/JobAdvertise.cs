using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class JobAdvertise
{
    public int Id { get; set; }

    public int JobId { get; set; }

    public int AdvertiseId { get; set; }

    public virtual Advertise Advertise { get; set; } = null!;
}
