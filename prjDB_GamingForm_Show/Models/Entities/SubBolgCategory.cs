using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class SubBolgCategory
{
    public int CategoryId { get; set; }

    public string Title { get; set; } = null!;

    public int SubBlogId { get; set; }
}
