using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class AdminRank
{
    public int RankId { get; set; }

    public bool RkProduct { get; set; }

    public bool RkMember { get; set; }

    public bool RkBlog { get; set; }

    public bool RkFirm { get; set; }

    public bool RkOrder { get; set; }

    public virtual ICollection<Admin> Admins { get; set; } = new List<Admin>();
}
