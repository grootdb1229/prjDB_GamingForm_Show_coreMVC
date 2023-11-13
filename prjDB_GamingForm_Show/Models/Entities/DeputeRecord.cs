using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class DeputeRecord
{
    public int Id { get; set; }

    public int DeputeId { get; set; }

    public int MemberId { get; set; }

    public int ApplyStatusId { get; set; }

    public string? RecordContent { get; set; }

    public virtual Status ApplyStatus { get; set; } = null!;

    public virtual Depute Depute { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
