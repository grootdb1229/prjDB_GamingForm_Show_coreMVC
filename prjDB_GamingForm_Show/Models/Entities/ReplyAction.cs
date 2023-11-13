using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ReplyAction
{
    public int Id { get; set; }

    public int MemberId { get; set; }

    public int ReplyId { get; set; }

    public int ActionId { get; set; }
}
