using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Image
{
    public int ImageId { get; set; }

    public string? Name { get; set; }

    public string Image1 { get; set; } = null!;

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
