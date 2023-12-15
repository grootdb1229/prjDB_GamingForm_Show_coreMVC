using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Taiwan
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Depute> Deputes { get; set; } = new List<Depute>();
}
