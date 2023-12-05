using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class MemberCollection
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int? CollectionId { get; set; }

    public string Title { get; set; } = null!;

    public string Intro { get; set; } = null!;

    public string? FImagePath { get; set; }

    public string MyCollection { get; set; } = null!;

    public string ModifiedDate { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
