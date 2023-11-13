using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class SubBlog
{
    public int SubBlogId { get; set; }

    public string Title { get; set; } = null!;

    public int BlogId { get; set; }

    public int? ParentBlogId { get; set; }

    public bool IsMemberBlog { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Blog Blog { get; set; } = null!;
}
