using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Image
{
    public int ImageId { get; set; }

    public string? Name { get; set; }

    public string Image1 { get; set; } = null!;
}
