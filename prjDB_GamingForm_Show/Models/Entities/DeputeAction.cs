using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class DeputeAction
{
    public int Id { get; set; }

    public int DeputeId { get; set; }

    public int MemberId { get; set; }

    public int ActionId { get; set; }

    public virtual ArticleAction Action { get; set; } = null!;

    public virtual Depute Depute { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
