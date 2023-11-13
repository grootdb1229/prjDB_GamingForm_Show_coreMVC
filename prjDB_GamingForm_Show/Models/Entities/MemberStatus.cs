using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class MemberStatus
{
    public int Id { get; set; }

    public int MemeberId { get; set; }

    public int StatusId { get; set; }

    public virtual Member Memeber { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
