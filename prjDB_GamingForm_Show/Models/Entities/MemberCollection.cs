using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class MemberCollection
{
    public int Id { get; set; }

    public int MemberId { get; set; }
    
    public int? CollectionId { get; set; }
    [DisplayName("標題")]
    public string Title { get; set; } = null!;
    [DisplayName("介紹")]
    public string Intro { get; set; } = null!;

    public string? FImagePath { get; set; }
    [DisplayName("作品介紹")]
    public string MyCollection { get; set; } = null!;

    public string ModifiedDate { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
