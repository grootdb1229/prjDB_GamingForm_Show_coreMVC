using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class MemberChat
{
    public int Id { get; set; }

    public int SenderMember { get; set; }

    public int ReceiveMember { get; set; }

    public string ChatContent { get; set; } = null!;

    public string ModefiedDate { get; set; } = null!;

    public bool IsCheck { get; set; }

    public virtual Member ReceiveMemberNavigation { get; set; } = null!;

    public virtual Member SenderMemberNavigation { get; set; } = null!;
}
