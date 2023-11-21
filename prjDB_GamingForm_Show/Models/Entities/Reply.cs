using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Reply
{
    public int ReplyId { get; set; }

    public int ArticleId { get; set; }
    [DisplayName("回覆內容")]
    public string ReplyContents { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public int MemberId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual Member Member { get; set; } = null!;
}
