using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Skill
{
    public int SkillId { get; set; }

    public int SkillClassId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<DeputeSkill> DeputeSkills { get; set; } = new List<DeputeSkill>();

    public virtual SkillClass SkillClass { get; set; } = null!;
}
