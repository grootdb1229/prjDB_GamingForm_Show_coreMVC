using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Product
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public DateTime AvailableDate { get; set; }

    public string? ProductContent { get; set; }

    public int UnitStock { get; set; }

    public int StatusId { get; set; }

    public int? MemberId { get; set; }

    public int? FirmId { get; set; }

    public string? FImagePath { get; set; }

    public virtual Member? Member { get; set; }

    public virtual ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();

    public virtual ICollection<ProductAdvertise> ProductAdvertises { get; set; } = new List<ProductAdvertise>();

    public virtual ICollection<ProductComplain> ProductComplains { get; set; } = new List<ProductComplain>();

    public virtual ICollection<ProductEvaluate> ProductEvaluates { get; set; } = new List<ProductEvaluate>();

    public virtual ICollection<ProductTag> ProductTags { get; set; } = new List<ProductTag>();

    public virtual Status? Status { get; set; } = null!;

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
