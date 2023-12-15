using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class NewsLetter
{
    public int Id { get; set; }

    public int Title { get; set; }

    public int HtmlContent { get; set; }
}
