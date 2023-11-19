using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models;

public partial class TProduct
{
    public int FId { get; set; }

    public string? FName { get; set; }

    public int? FQty { get; set; }

    public decimal? FCost { get; set; }

    public decimal? FPrice { get; set; }

    public string? FImagePath { get; set; }
}
