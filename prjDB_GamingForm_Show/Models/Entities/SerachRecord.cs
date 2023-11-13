using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class SerachRecord
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreateDays { get; set; }

    public bool? IsMember { get; set; }
}
