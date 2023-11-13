using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class DeputeSkill
{
    public int Id { get; set; }

    public int DeputeId { get; set; }

    public int SkillId { get; set; }

    public virtual Depute Depute { get; set; } = null!;

    public virtual Skill Skill { get; set; } = null!;
}
