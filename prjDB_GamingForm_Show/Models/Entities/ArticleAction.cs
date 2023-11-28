using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ArticleAction
{
    public int Id { get; set; }

    public int ArticleId { get; set; }

    public int MemberId { get; set; }

    public int ActionId { get; set; }

    public virtual Action Action { get; set; } = null!;

    public virtual Article Article { get; set; } = null!;

    public virtual ICollection<DeputeAction> DeputeActions { get; set; } = new List<DeputeAction>();

    public virtual Member Member { get; set; } = null!;
}
