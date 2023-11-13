using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class MemberTag
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int? TagId { get; set; }

    public int? SubTagId { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual SubTag? SubTag { get; set; }

    public virtual Tag? Tag { get; set; }
}
