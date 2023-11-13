using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class SkillClass
{
    public int SkillClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Skill> Skills { get; set; } = new List<Skill>();
}
