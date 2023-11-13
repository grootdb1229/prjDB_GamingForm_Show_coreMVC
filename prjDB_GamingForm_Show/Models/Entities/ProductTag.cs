using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ProductTag
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int SubTagId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual SubTag SubTag { get; set; } = null!;
}
