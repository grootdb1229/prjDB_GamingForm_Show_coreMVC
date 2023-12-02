using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Chat
{
    public int Id { get; set; }

    public int SenderAdmin { get; set; }

    public int ReceiveAdmin { get; set; }

    public string ChatContent { get; set; } = null!;

    public string ModefiedDate { get; set; } = null!;

    public bool IsCheck { get; set; }

    public virtual Admin ReceiveAdminNavigation { get; set; } = null!;

    public virtual Admin SenderAdminNavigation { get; set; } = null!;
}
