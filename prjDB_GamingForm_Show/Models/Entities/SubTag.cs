using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class SubTag
{
    public int SubTagId { get; set; }

    public string Name { get; set; } = null!;

    public int TagId { get; set; }

    public virtual ICollection<ArticleComplain> ArticleComplains { get; set; } = new List<ArticleComplain>();

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<DeputeComplain> DeputeComplains { get; set; } = new List<DeputeComplain>();

    public virtual ICollection<MemberTag> MemberTags { get; set; } = new List<MemberTag>();

    public virtual ICollection<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    public virtual Tag Tag { get; set; } = null!;
}
