using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ReplyImage
{
    public int Id { get; set; }

    public int ReplyId { get; set; }

    public int ImageId { get; set; }
}
