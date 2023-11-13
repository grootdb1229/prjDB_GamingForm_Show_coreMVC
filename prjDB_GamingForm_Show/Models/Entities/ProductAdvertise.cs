using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ProductAdvertise
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int AdvertiseId { get; set; }

    public virtual Advertise Advertise { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
