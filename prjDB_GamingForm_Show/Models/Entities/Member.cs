using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Member
{
    public int MemberId { get; set; }
    [DisplayName("會員名稱")]
    public string Name { get; set; } = null!;
    [DisplayName("會員手機")]
    public string Phone { get; set; } = null!;
    [DisplayName("生日")]
    public DateTime Birth { get; set; }
    [DisplayName("電子信箱")]
    public string Email { get; set; } = null!;
    [DisplayName("密碼")]
    public string Password { get; set; } = null!;
    public string FImagePath { get; set; } = null!;
    [DisplayName("自我介紹")]
    public string? Mycomment { get; set; }

    public int BonusPoint { get; set; }
    [DisplayName("性別")]
    public int Gender { get; set; }

    public int StatusId { get; set; }

    public virtual ICollection<ArticleAction> ArticleActions { get; set; } = new List<ArticleAction>();

    public virtual ICollection<ArticleComplain> ArticleComplains { get; set; } = new List<ArticleComplain>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<DeputeAction> DeputeActions { get; set; } = new List<DeputeAction>();

    public virtual ICollection<DeputeComplain> DeputeComplains { get; set; } = new List<DeputeComplain>();

    public virtual ICollection<DeputeRecord> DeputeRecords { get; set; } = new List<DeputeRecord>();

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();

    public virtual Gender GenderNavigation { get; set; } = null!;

    public virtual ICollection<MemberChat> MemberChatReceiveMemberNavigations { get; set; } = new List<MemberChat>();

    public virtual ICollection<MemberChat> MemberChatSenderMemberNavigations { get; set; } = new List<MemberChat>();

    public virtual ICollection<MemberCollection> MemberCollections { get; set; } = new List<MemberCollection>();

    public virtual ICollection<MemberCoupon> MemberCoupons { get; set; } = new List<MemberCoupon>();

    public virtual ICollection<MemberTag> MemberTags { get; set; } = new List<MemberTag>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

    public virtual ICollection<ProductEvaluate> ProductEvaluates { get; set; } = new List<ProductEvaluate>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<PublicChat> PublicChats { get; set; } = new List<PublicChat>();

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
