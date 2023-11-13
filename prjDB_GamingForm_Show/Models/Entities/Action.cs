using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Action
{
    public int ActionId { get; set; }

    public string? ActionType { get; set; }

    public virtual ICollection<ArticleAction> ArticleActions { get; set; } = new List<ArticleAction>();
}
