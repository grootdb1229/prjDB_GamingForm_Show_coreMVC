using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class ResumeSkill
{
    public int Id { get; set; }

    public int ResumeId { get; set; }

    public int SkillId { get; set; }
}
