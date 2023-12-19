using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class NewsLetter
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string HtmlContent { get; set; } = null!;
}
