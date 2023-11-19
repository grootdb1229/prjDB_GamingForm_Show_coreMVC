using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Blog
{
    public int BlogId { get; set; }

    public string Title { get; set; } = null!;

    public int SubTagId { get; set; }

    public string? FImagePath { get; set; }

    public virtual ICollection<SubBlog> SubBlogs { get; set; } = new List<SubBlog>();

    public virtual SubTag SubTag { get; set; } = null!;
}
