using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ProductComplain
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ReplyContent { get; set; } = null!;

    public int MemeberId { get; set; }

    public DateTime ReportDate { get; set; }

    public int SubTagId { get; set; }

    public int StatusId { get; set; }

    public virtual Member Memeber { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual SubTag SubTag { get; set; } = null!;
}
