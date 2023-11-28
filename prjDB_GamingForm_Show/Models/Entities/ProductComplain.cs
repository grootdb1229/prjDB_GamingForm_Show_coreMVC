using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ProductComplain
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int ReplyContent { get; set; }

    public int MemeberId { get; set; }

    public string ReportDate { get; set; } = null!;

    public virtual Member Memeber { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
