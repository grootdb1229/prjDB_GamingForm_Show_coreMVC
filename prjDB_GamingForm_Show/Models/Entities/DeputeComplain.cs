using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class DeputeComplain
{
    public int Id { get; set; }

    public int DeputeId { get; set; }

    public int MemberId { get; set; }

    public string? ReportContent { get; set; }

    public DateTime ReportDate { get; set; }

    public int SubTag { get; set; }
}
