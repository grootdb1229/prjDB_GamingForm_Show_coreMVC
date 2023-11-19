using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models;

public partial class TCustomer
{
    public int FId { get; set; }

    public string? FName { get; set; }

    public string? FPhone { get; set; }

    public string? FEmail { get; set; }

    public string? FAddress { get; set; }

    public string? FPassword { get; set; }
}
