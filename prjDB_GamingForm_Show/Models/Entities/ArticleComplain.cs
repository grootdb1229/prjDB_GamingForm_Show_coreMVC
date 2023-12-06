using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ArticleComplain
{
    public int Id { get; set; }

    public int ArticleId { get; set; }

    public int MemberId { get; set; }

    public string? ReportContent { get; set; }

    public DateTime ReportDate { get; set; }

    public int SubTagId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;

    public virtual SubTag SubTag { get; set; } = null!;
}
