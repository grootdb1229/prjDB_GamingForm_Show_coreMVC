using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Tag
{
    public int TagId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<MemberTag> MemberTags { get; set; } = new List<MemberTag>();

    public virtual ICollection<SubTag> SubTags { get; set; } = new List<SubTag>();
}
