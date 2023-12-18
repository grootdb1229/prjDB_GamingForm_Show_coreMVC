using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace prjDB_GamingForm_Show.Migrations
{
    /// <inheritdoc />
    public partial class mssql_migration_689 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Action",
                columns: table => new
                {
                    ActionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Action", x => x.ActionID);
                });

            migrationBuilder.CreateTable(
                name: "AdminRank",
                columns: table => new
                {
                    RankID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RK_Product = table.Column<bool>(type: "bit", nullable: false),
                    RK_Member = table.Column<bool>(type: "bit", nullable: false),
                    RK_Blog = table.Column<bool>(type: "bit", nullable: false),
                    RK_Firm = table.Column<bool>(type: "bit", nullable: false),
                    RK_Order = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRank", x => x.RankID);
                });

            migrationBuilder.CreateTable(
                name: "EcpayOrders",
                columns: table => new
                {
                    MerchantTradeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RtnCode = table.Column<int>(type: "int", nullable: true),
                    RtnMsg = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TradeNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TradeAmt = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PaymentTypeChargeFee = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TradeDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SimulatePaid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EcpayOrders", x => x.MerchantTradeNo);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Firm", x => x.GenderID);
                });

            migrationBuilder.CreateTable(
                name: "Image",
                columns: table => new
                {
                    ImageID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.ImageID);
                });

            migrationBuilder.CreateTable(
                name: "MemberStatus",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemeberID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberStatus", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "NewsLetter",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<int>(type: "int", nullable: false),
                    HtmlContent = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAdvertise", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.PaymentID);
                });

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    RegionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.RegionID);
                });

            migrationBuilder.CreateTable(
                name: "SerachRecord",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDays = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SerachRecord", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SkillClass",
                columns: table => new
                {
                    SkillClassID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillClass", x => x.SkillClassID);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    TagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.TagID);
                });

            migrationBuilder.CreateTable(
                name: "Admin",
                columns: table => new
                {
                    AdminID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAccount = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    RankID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImgPath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admin", x => x.AdminID);
                    table.ForeignKey(
                        name: "FK_Admin_AdminRank",
                        column: x => x.RankID,
                        principalTable: "AdminRank",
                        principalColumn: "RankID");
                });

            migrationBuilder.CreateTable(
                name: "RegionDistrict",
                columns: table => new
                {
                    Zipcode = table.Column<int>(type: "int", nullable: false),
                    RegionID = table.Column<int>(type: "int", nullable: false),
                    District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionDistrict_1", x => x.Zipcode);
                    table.ForeignKey(
                        name: "FK_RegionDistrict_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID");
                });

            migrationBuilder.CreateTable(
                name: "Skill",
                columns: table => new
                {
                    SkillID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillClassID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skill", x => x.SkillID);
                    table.ForeignKey(
                        name: "FK_Skill_SkillClass",
                        column: x => x.SkillClassID,
                        principalTable: "SkillClass",
                        principalColumn: "SkillClassID");
                });

            migrationBuilder.CreateTable(
                name: "Advertise",
                columns: table => new
                {
                    AdvertiseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AdContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AD", x => x.AdvertiseID);
                    table.ForeignKey(
                        name: "FK_AD_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CouponContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discount = table.Column<double>(type: "float", nullable: true),
                    Reduce = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EndDate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certificate", x => x.CouponID);
                    table.ForeignKey(
                        name: "FK_Coupon_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Member",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: false),
                    Birth = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mycomment = table.Column<string>(type: "ntext", nullable: true),
                    BonusPoint = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((3))"),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Member", x => x.MemberID);
                    table.ForeignKey(
                        name: "FK_Member_Gender",
                        column: x => x.Gender,
                        principalTable: "Gender",
                        principalColumn: "GenderID");
                    table.ForeignKey(
                        name: "FK_Member_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "SubTag",
                columns: table => new
                {
                    SubTagID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubTag", x => x.SubTagID);
                    table.ForeignKey(
                        name: "FK_SubTag_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderAdmin = table.Column<int>(type: "int", nullable: false),
                    ReceiveAdmin = table.Column<int>(type: "int", nullable: false),
                    ChatContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModefiedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Chat_Admin",
                        column: x => x.SenderAdmin,
                        principalTable: "Admin",
                        principalColumn: "AdminID");
                    table.ForeignKey(
                        name: "FK_Chat_Admin1",
                        column: x => x.ReceiveAdmin,
                        principalTable: "Admin",
                        principalColumn: "AdminID");
                });

            migrationBuilder.CreateTable(
                name: "JobAdvertise",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    JobID = table.Column<int>(type: "int", nullable: false),
                    AdvertiseID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAdvertise", x => x.ID);
                    table.ForeignKey(
                        name: "FK_JobAdvertise_AD",
                        column: x => x.AdvertiseID,
                        principalTable: "Advertise",
                        principalColumn: "AdvertiseID");
                });

            migrationBuilder.CreateTable(
                name: "Depute",
                columns: table => new
                {
                    DeputeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modifiedate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DeputeContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salary = table.Column<decimal>(type: "money", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((4))"),
                    RegionID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job Opportunities", x => x.DeputeID);
                    table.ForeignKey(
                        name: "FK_Depute_Member",
                        column: x => x.ProviderID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_Depute_Region",
                        column: x => x.RegionID,
                        principalTable: "Region",
                        principalColumn: "RegionID");
                    table.ForeignKey(
                        name: "FK_Depute_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "MemberChat",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderMember = table.Column<int>(type: "int", nullable: false),
                    ReceiveMember = table.Column<int>(type: "int", nullable: false),
                    ChatContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModefiedDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCheck = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberChat", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberChat_Member",
                        column: x => x.SenderMember,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_MemberChat_Member1",
                        column: x => x.ReceiveMember,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateTable(
                name: "MemberCollection",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    CollectionID = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Intro = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MyCollection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCollection", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberCollection_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateTable(
                name: "MemberCoupon",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    CouponID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberCoupon", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberCoupon_Coupon",
                        column: x => x.CouponID,
                        principalTable: "Coupon",
                        principalColumn: "CouponID");
                    table.ForeignKey(
                        name: "FK_MemberCoupon_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: true),
                    CouponID = table.Column<int>(type: "int", nullable: true),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "date", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "date", nullable: true),
                    PaymentID = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    SumPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.OrderID);
                    table.ForeignKey(
                        name: "FK_Order_Coupon",
                        column: x => x.CouponID,
                        principalTable: "Coupon",
                        principalColumn: "CouponID");
                    table.ForeignKey(
                        name: "FK_Order_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_Order_Payment",
                        column: x => x.PaymentID,
                        principalTable: "Payment",
                        principalColumn: "PaymentID");
                    table.ForeignKey(
                        name: "FK_Order_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    AvailableDate = table.Column<DateTime>(type: "date", nullable: false),
                    ProductContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitStock = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goods = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.ForeignKey(
                        name: "FK_Product_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_Product_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "Blog",
                columns: table => new
                {
                    BlogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubTagID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((10))"),
                    fImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blog", x => x.BlogID);
                    table.ForeignKey(
                        name: "FK_Blog_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                });

            migrationBuilder.CreateTable(
                name: "MemberTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    TagID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    SubTagID = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberTag", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MemberTag_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_MemberTag_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                    table.ForeignKey(
                        name: "FK_MemberTag_Tag",
                        column: x => x.TagID,
                        principalTable: "Tag",
                        principalColumn: "TagID");
                });

            migrationBuilder.CreateTable(
                name: "DeputeAction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeputeID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputeAction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeputeAction_Action",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK_DeputeAction_Depute",
                        column: x => x.DeputeID,
                        principalTable: "Depute",
                        principalColumn: "DeputeID");
                    table.ForeignKey(
                        name: "FK_DeputeAction_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateTable(
                name: "DeputeComplain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeputeID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubTagID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((28))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeputeComplain", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeputeComplain_Depute",
                        column: x => x.DeputeID,
                        principalTable: "Depute",
                        principalColumn: "DeputeID");
                    table.ForeignKey(
                        name: "FK_DeputeComplain_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_DeputeComplain_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_DeputeComplain_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                });

            migrationBuilder.CreateTable(
                name: "DeputeRecord",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeputeID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ApplyStatusID = table.Column<int>(type: "int", nullable: false),
                    RecordContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReplyFileName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobResume", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeputeRecord_Depute",
                        column: x => x.DeputeID,
                        principalTable: "Depute",
                        principalColumn: "DeputeID");
                    table.ForeignKey(
                        name: "FK_DeputeRecord_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_DeputeRecord_Status",
                        column: x => x.ApplyStatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "DeputeSkill",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeputeID = table.Column<int>(type: "int", nullable: false),
                    SkillID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSkill", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DeputeSkill_Depute",
                        column: x => x.DeputeID,
                        principalTable: "Depute",
                        principalColumn: "DeputeID");
                    table.ForeignKey(
                        name: "FK_DeputeSkill_Skill",
                        column: x => x.SkillID,
                        principalTable: "Skill",
                        principalColumn: "SkillID");
                });

            migrationBuilder.CreateTable(
                name: "OrderProduct",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    Quantinty = table.Column<int>(type: "int", nullable: false),
                    Disconut = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProduct", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OrderProduct_Order",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "OrderID");
                    table.ForeignKey(
                        name: "FK_OrderProduct_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductComplain",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReplyContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MemeberId = table.Column<int>(type: "int", nullable: false),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubTagID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((28))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductComplain", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductComplain_Member",
                        column: x => x.MemeberId,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_ProductComplain_Product",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductComplain_Status",
                        column: x => x.StatusID,
                        principalTable: "Status",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_ProductComplain_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                });

            migrationBuilder.CreateTable(
                name: "ProductEvaluate",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    EvaLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EvaContent = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductEvaluate", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductEvaluate_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_ProductEvaluate_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductImage",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    ImageID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImage", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductImage_Image",
                        column: x => x.ImageID,
                        principalTable: "Image",
                        principalColumn: "ImageID");
                    table.ForeignKey(
                        name: "FK_ProductImage_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    SubTagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ProductAction_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                    table.ForeignKey(
                        name: "FK_ProductAction_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                });

            migrationBuilder.CreateTable(
                name: "WishList",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ProductID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishList", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WishList_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_WishList_Product",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID");
                });

            migrationBuilder.CreateTable(
                name: "SubBlog",
                columns: table => new
                {
                    SubBlogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    BlogID = table.Column<int>(type: "int", nullable: false),
                    ParentBlogID = table.Column<int>(type: "int", nullable: true),
                    IsMemberBlog = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubBlog", x => x.SubBlogID);
                    table.ForeignKey(
                        name: "FK_SubBlog_Blog",
                        column: x => x.BlogID,
                        principalTable: "Blog",
                        principalColumn: "BlogID");
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    ArticleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubBlogID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArticleContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ReplyArticleID = table.Column<int>(type: "int", nullable: true),
                    ViewCount = table.Column<int>(type: "int", nullable: false),
                    IsPinned = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_Article_Article",
                        column: x => x.ReplyArticleID,
                        principalTable: "Article",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_Article_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_Article_SubBlog",
                        column: x => x.SubBlogID,
                        principalTable: "SubBlog",
                        principalColumn: "SubBlogID");
                });

            migrationBuilder.CreateTable(
                name: "ArticleAction",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ActionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogAction", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArticleAction_Action",
                        column: x => x.ActionID,
                        principalTable: "Action",
                        principalColumn: "ActionID");
                    table.ForeignKey(
                        name: "FK_ArticleAction_Article",
                        column: x => x.ArticleID,
                        principalTable: "Article",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_ArticleAction_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateTable(
                name: "ArticleComplain",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false),
                    ReportContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    SubTagID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleComplain", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ArticleComplain_Article",
                        column: x => x.ArticleID,
                        principalTable: "Article",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_ArticleComplain_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                    table.ForeignKey(
                        name: "FK_ArticleComplain_SubTag",
                        column: x => x.SubTagID,
                        principalTable: "SubTag",
                        principalColumn: "SubTagID");
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    ReplyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArticleID = table.Column<int>(type: "int", nullable: false),
                    ReplyContents = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    MemberID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.ReplyID);
                    table.ForeignKey(
                        name: "FK_Reply_Article",
                        column: x => x.ArticleID,
                        principalTable: "Article",
                        principalColumn: "ArticleID");
                    table.ForeignKey(
                        name: "FK_Reply_Member",
                        column: x => x.MemberID,
                        principalTable: "Member",
                        principalColumn: "MemberID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admin_RankID",
                table: "Admin",
                column: "RankID");

            migrationBuilder.CreateIndex(
                name: "IX_Advertise_StatusID",
                table: "Advertise",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_MemberID",
                table: "Article",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ReplyArticleID",
                table: "Article",
                column: "ReplyArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Article_SubBlogID",
                table: "Article",
                column: "SubBlogID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAction_ActionID",
                table: "ArticleAction",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAction_ArticleID",
                table: "ArticleAction",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleAction_MemberID",
                table: "ArticleAction",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComplain_ArticleID",
                table: "ArticleComplain",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComplain_MemberID",
                table: "ArticleComplain",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleComplain_SubTagID",
                table: "ArticleComplain",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_Blog_SubTagID",
                table: "Blog",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_ReceiveAdmin",
                table: "Chat",
                column: "ReceiveAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_SenderAdmin",
                table: "Chat",
                column: "SenderAdmin");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_StatusID",
                table: "Coupon",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_Depute_ProviderID",
                table: "Depute",
                column: "ProviderID");

            migrationBuilder.CreateIndex(
                name: "IX_Depute_RegionID",
                table: "Depute",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Depute_StatusID",
                table: "Depute",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeAction_ActionID",
                table: "DeputeAction",
                column: "ActionID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeAction_DeputeID",
                table: "DeputeAction",
                column: "DeputeID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeAction_MemberID",
                table: "DeputeAction",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeComplain_DeputeID",
                table: "DeputeComplain",
                column: "DeputeID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeComplain_MemberID",
                table: "DeputeComplain",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeComplain_StatusID",
                table: "DeputeComplain",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeComplain_SubTagID",
                table: "DeputeComplain",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeRecord_ApplyStatusID",
                table: "DeputeRecord",
                column: "ApplyStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeRecord_DeputeID",
                table: "DeputeRecord",
                column: "DeputeID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeRecord_MemberID",
                table: "DeputeRecord",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeSkill_DeputeID",
                table: "DeputeSkill",
                column: "DeputeID");

            migrationBuilder.CreateIndex(
                name: "IX_DeputeSkill_SkillID",
                table: "DeputeSkill",
                column: "SkillID");

            migrationBuilder.CreateIndex(
                name: "IX_JobAdvertise_AdvertiseID",
                table: "JobAdvertise",
                column: "AdvertiseID");

            migrationBuilder.CreateIndex(
                name: "IX_Member_Gender",
                table: "Member",
                column: "Gender");

            migrationBuilder.CreateIndex(
                name: "IX_Member_StatusID",
                table: "Member",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberChat_ReceiveMember",
                table: "MemberChat",
                column: "ReceiveMember");

            migrationBuilder.CreateIndex(
                name: "IX_MemberChat_SenderMember",
                table: "MemberChat",
                column: "SenderMember");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCollection_MemberID",
                table: "MemberCollection",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCoupon_CouponID",
                table: "MemberCoupon",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberCoupon_MemberID",
                table: "MemberCoupon",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTag_MemberID",
                table: "MemberTag",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTag_SubTagID",
                table: "MemberTag",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_MemberTag_TagID",
                table: "MemberTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_CouponID",
                table: "Order",
                column: "CouponID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_MemberID",
                table: "Order",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_PaymentID",
                table: "Order",
                column: "PaymentID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_StatusID",
                table: "Order",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_OrderID",
                table: "OrderProduct",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProduct_ProductID",
                table: "OrderProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_MemberID",
                table: "Product",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Product_StatusID",
                table: "Product",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComplain_MemeberId",
                table: "ProductComplain",
                column: "MemeberId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComplain_ProductId",
                table: "ProductComplain",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComplain_StatusID",
                table: "ProductComplain",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductComplain_SubTagID",
                table: "ProductComplain",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluate_MemberID",
                table: "ProductEvaluate",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductEvaluate_ProductID",
                table: "ProductEvaluate",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ImageID",
                table: "ProductImage",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductImage_ProductID",
                table: "ProductImage",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_ProductID",
                table: "ProductTag",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_SubTagID",
                table: "ProductTag",
                column: "SubTagID");

            migrationBuilder.CreateIndex(
                name: "IX_RegionDistrict_RegionID",
                table: "RegionDistrict",
                column: "RegionID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_ArticleID",
                table: "Reply",
                column: "ArticleID");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_MemberID",
                table: "Reply",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_Skill_SkillClassID",
                table: "Skill",
                column: "SkillClassID");

            migrationBuilder.CreateIndex(
                name: "IX_SubBlog_BlogID",
                table: "SubBlog",
                column: "BlogID");

            migrationBuilder.CreateIndex(
                name: "IX_SubTag_TagID",
                table: "SubTag",
                column: "TagID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_MemberID",
                table: "WishList",
                column: "MemberID");

            migrationBuilder.CreateIndex(
                name: "IX_WishList_ProductID",
                table: "WishList",
                column: "ProductID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleAction");

            migrationBuilder.DropTable(
                name: "ArticleComplain");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "DeputeAction");

            migrationBuilder.DropTable(
                name: "DeputeComplain");

            migrationBuilder.DropTable(
                name: "DeputeRecord");

            migrationBuilder.DropTable(
                name: "DeputeSkill");

            migrationBuilder.DropTable(
                name: "EcpayOrders");

            migrationBuilder.DropTable(
                name: "JobAdvertise");

            migrationBuilder.DropTable(
                name: "MemberChat");

            migrationBuilder.DropTable(
                name: "MemberCollection");

            migrationBuilder.DropTable(
                name: "MemberCoupon");

            migrationBuilder.DropTable(
                name: "MemberStatus");

            migrationBuilder.DropTable(
                name: "MemberTag");

            migrationBuilder.DropTable(
                name: "NewsLetter");

            migrationBuilder.DropTable(
                name: "OrderProduct");

            migrationBuilder.DropTable(
                name: "ProductComplain");

            migrationBuilder.DropTable(
                name: "ProductEvaluate");

            migrationBuilder.DropTable(
                name: "ProductImage");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "RegionDistrict");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "SerachRecord");

            migrationBuilder.DropTable(
                name: "WishList");

            migrationBuilder.DropTable(
                name: "Admin");

            migrationBuilder.DropTable(
                name: "Action");

            migrationBuilder.DropTable(
                name: "Depute");

            migrationBuilder.DropTable(
                name: "Skill");

            migrationBuilder.DropTable(
                name: "Advertise");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Image");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "AdminRank");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "SkillClass");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DropTable(
                name: "SubBlog");

            migrationBuilder.DropTable(
                name: "Member");

            migrationBuilder.DropTable(
                name: "Blog");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "SubTag");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
