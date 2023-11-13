using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Firm
{
    public int FirmId { get; set; }

    public string FirmName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int TaxId { get; set; }

    public string Password { get; set; } = null!;

    public string FirmAddress { get; set; } = null!;

    public string FirmScale { get; set; } = null!;

    public string? FirmIntro { get; set; }

    public string Contact { get; set; } = null!;

    public int? StatusId { get; set; }

    public int? ImageId { get; set; }
}
