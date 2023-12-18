using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class PublicChat
{
    public int Id { get; set; }

    public int SenderId { get; set; }

    public string ChatContent { get; set; } = null!;

    public string Modifiedate { get; set; } = null!;

    public virtual Member Sender { get; set; } = null!;
}
