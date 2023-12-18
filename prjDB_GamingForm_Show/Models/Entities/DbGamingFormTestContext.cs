using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class DbGamingFormTestContext : DbContext
{
    public DbGamingFormTestContext()
    {
    }

    public DbGamingFormTestContext(DbContextOptions<DbGamingFormTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Action> Actions { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<AdminRank> AdminRanks { get; set; }

    public virtual DbSet<Advertise> Advertises { get; set; }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<ArticleAction> ArticleActions { get; set; }

    public virtual DbSet<ArticleComplain> ArticleComplains { get; set; }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Coupon> Coupons { get; set; }

    public virtual DbSet<Depute> Deputes { get; set; }

    public virtual DbSet<DeputeAction> DeputeActions { get; set; }

    public virtual DbSet<DeputeComplain> DeputeComplains { get; set; }

    public virtual DbSet<DeputeRecord> DeputeRecords { get; set; }

    public virtual DbSet<DeputeSkill> DeputeSkills { get; set; }

    public virtual DbSet<EcpayOrder> EcpayOrders { get; set; }

    public virtual DbSet<Gender> Genders { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<JobAdvertise> JobAdvertises { get; set; }

    public virtual DbSet<Member> Members { get; set; }

    public virtual DbSet<MemberChat> MemberChats { get; set; }

    public virtual DbSet<MemberCollection> MemberCollections { get; set; }

    public virtual DbSet<MemberCoupon> MemberCoupons { get; set; }

    public virtual DbSet<MemberStatus> MemberStatuses { get; set; }

    public virtual DbSet<MemberTag> MemberTags { get; set; }

    public virtual DbSet<NewsLetter> NewsLetters { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderProduct> OrderProducts { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductComplain> ProductComplains { get; set; }

    public virtual DbSet<ProductEvaluate> ProductEvaluates { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductTag> ProductTags { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<RegionDistrict> RegionDistricts { get; set; }

    public virtual DbSet<Reply> Replies { get; set; }

    public virtual DbSet<SerachRecord> SerachRecords { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    public virtual DbSet<SkillClass> SkillClasses { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<SubBlog> SubBlogs { get; set; }

    public virtual DbSet<SubTag> SubTags { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<WishList> WishLists { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=DB_GamingForm_test;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Action>(entity =>
        {
            entity.ToTable("Action");

            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.ActionType).HasMaxLength(50);
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.ToTable("Admin");

            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.AdminAccount).HasMaxLength(50);
            entity.Property(e => e.AdminPassword).HasMaxLength(16);
            entity.Property(e => e.RankId).HasColumnName("RankID");

            entity.HasOne(d => d.Rank).WithMany(p => p.Admins)
                .HasForeignKey(d => d.RankId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Admin_AdminRank");
        });

        modelBuilder.Entity<AdminRank>(entity =>
        {
            entity.HasKey(e => e.RankId);

            entity.ToTable("AdminRank");

            entity.Property(e => e.RankId).HasColumnName("RankID");
            entity.Property(e => e.RkBlog).HasColumnName("RK_Blog");
            entity.Property(e => e.RkFirm).HasColumnName("RK_Firm");
            entity.Property(e => e.RkMember).HasColumnName("RK_Member");
            entity.Property(e => e.RkOrder).HasColumnName("RK_Order");
            entity.Property(e => e.RkProduct).HasColumnName("RK_Product");
        });

        modelBuilder.Entity<Advertise>(entity =>
        {
            entity.HasKey(e => e.AdvertiseId).HasName("PK_AD");

            entity.ToTable("Advertise");

            entity.Property(e => e.AdvertiseId).HasColumnName("AdvertiseID");
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Status).WithMany(p => p.Advertises)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AD_Status");
        });

        modelBuilder.Entity<Article>(entity =>
        {
            entity.ToTable("Article");

            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.ReplyArticleId).HasColumnName("ReplyArticleID");
            entity.Property(e => e.SubBlogId).HasColumnName("SubBlogID");

            entity.HasOne(d => d.Member).WithMany(p => p.Articles)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Article_Member");

            entity.HasOne(d => d.ReplyArticle).WithMany(p => p.InverseReplyArticle)
                .HasForeignKey(d => d.ReplyArticleId)
                .HasConstraintName("FK_Article_Article");

            entity.HasOne(d => d.SubBlog).WithMany(p => p.Articles)
                .HasForeignKey(d => d.SubBlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Article_SubBlog");
        });

        modelBuilder.Entity<ArticleAction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_BlogAction");

            entity.ToTable("ArticleAction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Action).WithMany(p => p.ArticleActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleAction_Action");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleActions)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleAction_Article");

            entity.HasOne(d => d.Member).WithMany(p => p.ArticleActions)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleAction_Member");
        });

        modelBuilder.Entity<ArticleComplain>(entity =>
        {
            entity.ToTable("ArticleComplain");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ReportDate).HasColumnType("datetime");
            entity.Property(e => e.SubTagId).HasColumnName("SubTagID");

            entity.HasOne(d => d.Article).WithMany(p => p.ArticleComplains)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleComplain_Article");

            entity.HasOne(d => d.Member).WithMany(p => p.ArticleComplains)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleComplain_Member");

            entity.HasOne(d => d.SubTag).WithMany(p => p.ArticleComplains)
                .HasForeignKey(d => d.SubTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ArticleComplain_SubTag");
        });

        modelBuilder.Entity<Blog>(entity =>
        {
            entity.ToTable("Blog");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
            entity.Property(e => e.SubTagId)
                .HasDefaultValueSql("((10))")
                .HasColumnName("SubTagID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.SubTag).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.SubTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Blog_SubTag");
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.ToTable("Chat");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.ReceiveAdminNavigation).WithMany(p => p.ChatReceiveAdminNavigations)
                .HasForeignKey(d => d.ReceiveAdmin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_Admin1");

            entity.HasOne(d => d.SenderAdminNavigation).WithMany(p => p.ChatSenderAdminNavigations)
                .HasForeignKey(d => d.SenderAdmin)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chat_Admin");
        });

        modelBuilder.Entity<Coupon>(entity =>
        {
            entity.HasKey(e => e.CouponId).HasName("PK_Certificate");

            entity.ToTable("Coupon");

            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.EndDate).HasMaxLength(50);
            entity.Property(e => e.StartDate).HasMaxLength(50);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .HasColumnName("title");

            entity.HasOne(d => d.Status).WithMany(p => p.Coupons)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Coupon_Status");
        });

        modelBuilder.Entity<Depute>(entity =>
        {
            entity.HasKey(e => e.DeputeId).HasName("PK_Job Opportunities");

            entity.ToTable("Depute");

            entity.Property(e => e.DeputeId).HasColumnName("DeputeID");
            entity.Property(e => e.Modifiedate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ProviderId).HasColumnName("ProviderID");
            entity.Property(e => e.RegionId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("RegionID");
            entity.Property(e => e.Salary).HasColumnType("money");
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((4))")
                .HasColumnName("StatusID");

            entity.HasOne(d => d.Provider).WithMany(p => p.Deputes)
                .HasForeignKey(d => d.ProviderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Depute_Member");

            entity.HasOne(d => d.Region).WithMany(p => p.Deputes)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Depute_Region");

            entity.HasOne(d => d.Status).WithMany(p => p.Deputes)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Depute_Status");
        });

        modelBuilder.Entity<DeputeAction>(entity =>
        {
            entity.ToTable("DeputeAction");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ActionId).HasColumnName("ActionID");
            entity.Property(e => e.DeputeId).HasColumnName("DeputeID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Action).WithMany(p => p.DeputeActions)
                .HasForeignKey(d => d.ActionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeAction_Action");

            entity.HasOne(d => d.Depute).WithMany(p => p.DeputeActions)
                .HasForeignKey(d => d.DeputeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeAction_Depute");

            entity.HasOne(d => d.Member).WithMany(p => p.DeputeActions)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeAction_Member");
        });

        modelBuilder.Entity<DeputeComplain>(entity =>
        {
            entity.ToTable("DeputeComplain");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeputeId).HasColumnName("DeputeID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ReportDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((28))")
                .HasColumnName("StatusID");
            entity.Property(e => e.SubTagId).HasColumnName("SubTagID");

            entity.HasOne(d => d.Depute).WithMany(p => p.DeputeComplains)
                .HasForeignKey(d => d.DeputeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeComplain_Depute");

            entity.HasOne(d => d.Member).WithMany(p => p.DeputeComplains)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeComplain_Member");

            entity.HasOne(d => d.Status).WithMany(p => p.DeputeComplains)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeComplain_Status");

            entity.HasOne(d => d.SubTag).WithMany(p => p.DeputeComplains)
                .HasForeignKey(d => d.SubTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeComplain_SubTag");
        });

        modelBuilder.Entity<DeputeRecord>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_JobResume");

            entity.ToTable("DeputeRecord");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ApplyStatusId).HasColumnName("ApplyStatusID");
            entity.Property(e => e.DeputeId).HasColumnName("DeputeID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.ApplyStatus).WithMany(p => p.DeputeRecords)
                .HasForeignKey(d => d.ApplyStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeRecord_Status");

            entity.HasOne(d => d.Depute).WithMany(p => p.DeputeRecords)
                .HasForeignKey(d => d.DeputeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeRecord_Depute");

            entity.HasOne(d => d.Member).WithMany(p => p.DeputeRecords)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeRecord_Member");
        });

        modelBuilder.Entity<DeputeSkill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_JobSkill");

            entity.ToTable("DeputeSkill");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DeputeId).HasColumnName("DeputeID");
            entity.Property(e => e.SkillId).HasColumnName("SkillID");

            entity.HasOne(d => d.Depute).WithMany(p => p.DeputeSkills)
                .HasForeignKey(d => d.DeputeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeSkill_Depute");

            entity.HasOne(d => d.Skill).WithMany(p => p.DeputeSkills)
                .HasForeignKey(d => d.SkillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DeputeSkill_Skill");
        });

        modelBuilder.Entity<EcpayOrder>(entity =>
        {
            entity.HasKey(e => e.MerchantTradeNo);

            entity.Property(e => e.MerchantTradeNo).HasMaxLength(50);
            entity.Property(e => e.MemberId)
                .HasMaxLength(50)
                .HasColumnName("MemberID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentType).HasMaxLength(50);
            entity.Property(e => e.PaymentTypeChargeFee).HasMaxLength(50);
            entity.Property(e => e.RtnMsg).HasMaxLength(50);
            entity.Property(e => e.TradeDate).HasMaxLength(50);
            entity.Property(e => e.TradeNo).HasMaxLength(50);
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.HasKey(e => e.GenderId).HasName("PK_Firm");

            entity.ToTable("Gender");

            entity.Property(e => e.GenderId).HasColumnName("GenderID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.ToTable("Image");

            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
        });

        modelBuilder.Entity<JobAdvertise>(entity =>
        {
            entity.ToTable("JobAdvertise");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.AdvertiseId).HasColumnName("AdvertiseID");
            entity.Property(e => e.JobId).HasColumnName("JobID");

            entity.HasOne(d => d.Advertise).WithMany(p => p.JobAdvertises)
                .HasForeignKey(d => d.AdvertiseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobAdvertise_AD");
        });

        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Member");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Birth).HasColumnType("date");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
            entity.Property(e => e.Gender).HasDefaultValueSql("((3))");
            entity.Property(e => e.Mycomment).HasColumnType("ntext");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(24);
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((1))")
                .HasColumnName("StatusID");

            entity.HasOne(d => d.GenderNavigation).WithMany(p => p.Members)
                .HasForeignKey(d => d.Gender)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_Gender");

            entity.HasOne(d => d.Status).WithMany(p => p.Members)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Member_Status");
        });

        modelBuilder.Entity<MemberChat>(entity =>
        {
            entity.ToTable("MemberChat");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.ReceiveMemberNavigation).WithMany(p => p.MemberChatReceiveMemberNavigations)
                .HasForeignKey(d => d.ReceiveMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberChat_Member1");

            entity.HasOne(d => d.SenderMemberNavigation).WithMany(p => p.MemberChatSenderMemberNavigations)
                .HasForeignKey(d => d.SenderMember)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberChat_Member");
        });

        modelBuilder.Entity<MemberCollection>(entity =>
        {
            entity.ToTable("MemberCollection");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CollectionId).HasColumnName("CollectionID");
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
            entity.Property(e => e.Intro).HasMaxLength(50);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Member).WithMany(p => p.MemberCollections)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCollection_Member");
        });

        modelBuilder.Entity<MemberCoupon>(entity =>
        {
            entity.ToTable("MemberCoupon");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");

            entity.HasOne(d => d.Coupon).WithMany(p => p.MemberCoupons)
                .HasForeignKey(d => d.CouponId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCoupon_Coupon");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberCoupons)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberCoupon_Member");
        });

        modelBuilder.Entity<MemberStatus>(entity =>
        {
            entity.ToTable("MemberStatus");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MemeberId).HasColumnName("MemeberID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
        });

        modelBuilder.Entity<MemberTag>(entity =>
        {
            entity.ToTable("MemberTag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.SubTagId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SubTagID");
            entity.Property(e => e.TagId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("TagID");

            entity.HasOne(d => d.Member).WithMany(p => p.MemberTags)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MemberTag_Member");

            entity.HasOne(d => d.SubTag).WithMany(p => p.MemberTags)
                .HasForeignKey(d => d.SubTagId)
                .HasConstraintName("FK_MemberTag_SubTag");

            entity.HasOne(d => d.Tag).WithMany(p => p.MemberTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK_MemberTag_Tag");
        });

        modelBuilder.Entity<NewsLetter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductAdvertise");

            entity.ToTable("NewsLetter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order");

            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.CompletedDate).HasColumnType("date");
            entity.Property(e => e.CouponId).HasColumnName("CouponID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Note).HasMaxLength(50);
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.PaymentDate).HasColumnType("date");
            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.SumPrice).HasColumnType("money");

            entity.HasOne(d => d.Coupon).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CouponId)
                .HasConstraintName("FK_Order_Coupon");

            entity.HasOne(d => d.Member).WithMany(p => p.Orders)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Order_Member");

            entity.HasOne(d => d.Payment).WithMany(p => p.Orders)
                .HasForeignKey(d => d.PaymentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Payment");

            entity.HasOne(d => d.Status).WithMany(p => p.Orders)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Status");
        });

        modelBuilder.Entity<OrderProduct>(entity =>
        {
            entity.ToTable("OrderProduct");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.OrderId).HasColumnName("OrderID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UnitPrice).HasColumnType("money");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderProduct_Order");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderProduct_Product");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.ToTable("Payment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.AvailableDate).HasColumnType("date");
            entity.Property(e => e.FImagePath).HasColumnName("fImagePath");
            entity.Property(e => e.MemberId)
                .HasDefaultValueSql("((0))")
                .HasColumnName("MemberID");
            entity.Property(e => e.Price).HasColumnType("money");
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.StatusId).HasColumnName("StatusID");

            entity.HasOne(d => d.Member).WithMany(p => p.Products)
                .HasForeignKey(d => d.MemberId)
                .HasConstraintName("FK_Product_Member");

            entity.HasOne(d => d.Status).WithMany(p => p.Products)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Status");
        });

        modelBuilder.Entity<ProductComplain>(entity =>
        {
            entity.ToTable("ProductComplain");

            entity.Property(e => e.ReportDate).HasColumnType("datetime");
            entity.Property(e => e.StatusId)
                .HasDefaultValueSql("((28))")
                .HasColumnName("StatusID");
            entity.Property(e => e.SubTagId).HasColumnName("SubTagID");

            entity.HasOne(d => d.Memeber).WithMany(p => p.ProductComplains)
                .HasForeignKey(d => d.MemeberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComplain_Member");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductComplains)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComplain_Product");

            entity.HasOne(d => d.Status).WithMany(p => p.ProductComplains)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComplain_Status");

            entity.HasOne(d => d.SubTag).WithMany(p => p.ProductComplains)
                .HasForeignKey(d => d.SubTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductComplain_SubTag");
        });

        modelBuilder.Entity<ProductEvaluate>(entity =>
        {
            entity.ToTable("ProductEvaluate");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EvaLevel).HasMaxLength(50);
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Member).WithMany(p => p.ProductEvaluates)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductEvaluate_Member");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductEvaluates)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductEvaluate_Product");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.ToTable("ProductImage");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ImageId).HasColumnName("ImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Image).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Image");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductImage_Product");
        });

        modelBuilder.Entity<ProductTag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductAction");

            entity.ToTable("ProductTag");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SubTagId).HasColumnName("SubTagID");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductTags)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductAction_Product");

            entity.HasOne(d => d.SubTag).WithMany(p => p.ProductTags)
                .HasForeignKey(d => d.SubTagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductAction_SubTag");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.ToTable("Region");

            entity.Property(e => e.RegionId).HasColumnName("RegionID");
            entity.Property(e => e.City).HasMaxLength(50);
        });

        modelBuilder.Entity<RegionDistrict>(entity =>
        {
            entity.HasKey(e => e.Zipcode).HasName("PK_RegionDistrict_1");

            entity.ToTable("RegionDistrict");

            entity.Property(e => e.Zipcode).ValueGeneratedNever();
            entity.Property(e => e.District).HasMaxLength(50);
            entity.Property(e => e.RegionId).HasColumnName("RegionID");

            entity.HasOne(d => d.Region).WithMany(p => p.RegionDistricts)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RegionDistrict_Region");
        });

        modelBuilder.Entity<Reply>(entity =>
        {
            entity.ToTable("Reply");

            entity.Property(e => e.ReplyId).HasColumnName("ReplyID");
            entity.Property(e => e.ArticleId).HasColumnName("ArticleID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Article).WithMany(p => p.Replies)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reply_Article");

            entity.HasOne(d => d.Member).WithMany(p => p.Replies)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reply_Member");
        });

        modelBuilder.Entity<SerachRecord>(entity =>
        {
            entity.ToTable("SerachRecord");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreateDays)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.ToTable("Skill");

            entity.Property(e => e.SkillId).HasColumnName("SkillID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.SkillClassId).HasColumnName("SkillClassID");

            entity.HasOne(d => d.SkillClass).WithMany(p => p.Skills)
                .HasForeignKey(d => d.SkillClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Skill_SkillClass");
        });

        modelBuilder.Entity<SkillClass>(entity =>
        {
            entity.ToTable("SkillClass");

            entity.Property(e => e.SkillClassId).HasColumnName("SkillClassID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.ToTable("Status");

            entity.Property(e => e.StatusId).HasColumnName("StatusID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<SubBlog>(entity =>
        {
            entity.ToTable("SubBlog");

            entity.Property(e => e.SubBlogId).HasColumnName("SubBlogID");
            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.ParentBlogId).HasColumnName("ParentBlogID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Blog).WithMany(p => p.SubBlogs)
                .HasForeignKey(d => d.BlogId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubBlog_Blog");
        });

        modelBuilder.Entity<SubTag>(entity =>
        {
            entity.ToTable("SubTag");

            entity.Property(e => e.SubTagId).HasColumnName("SubTagID");
            entity.Property(e => e.TagId).HasColumnName("TagID");

            entity.HasOne(d => d.Tag).WithMany(p => p.SubTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubTag_Tag");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.ToTable("Tag");

            entity.Property(e => e.TagId).HasColumnName("TagID");
        });

        modelBuilder.Entity<WishList>(entity =>
        {
            entity.ToTable("WishList");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");

            entity.HasOne(d => d.Member).WithMany(p => p.WishLists)
                .HasForeignKey(d => d.MemberId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WishList_Member");

            entity.HasOne(d => d.Product).WithMany(p => p.WishLists)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_WishList_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
