using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ProductEvaluate
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int MemberId { get; set; }

    public string? EvaLevel { get; set; }

    public string? EvaContent { get; set; }

    public virtual Member Member { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
