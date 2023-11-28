using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Member
{
    public int MemberId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime Birth { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string FImagePath { get; set; } = null!;

    public string? Mycomment { get; set; }

    public int BonusPoint { get; set; }

    public int Gender { get; set; }

    public virtual ICollection<ArticleAction> ArticleActions { get; set; } = new List<ArticleAction>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<DeputeAction> DeputeActions { get; set; } = new List<DeputeAction>();

    public virtual ICollection<DeputeRecord> DeputeRecords { get; set; } = new List<DeputeRecord>();

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual ICollection<MemberStatus> MemberStatuses { get; set; } = new List<MemberStatus>();

    public virtual ICollection<MemberTag> MemberTags { get; set; } = new List<MemberTag>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

    public virtual ICollection<ProductEvaluate> ProductEvaluates { get; set; } = new List<ProductEvaluate>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
