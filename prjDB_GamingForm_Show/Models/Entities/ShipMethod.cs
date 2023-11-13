using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ShipMethod
{
    public int ShipId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
