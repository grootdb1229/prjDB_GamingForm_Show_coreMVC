using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Admin
{
    public int AdminId { get; set; }

    public string AdminAccount { get; set; } = null!;

    public string AdminPassword { get; set; } = null!;

    public int RankId { get; set; }

    public string? Name { get; set; }

    public string? ImgPath { get; set; }

    public virtual AdminRank Rank { get; set; } = null!;
}
