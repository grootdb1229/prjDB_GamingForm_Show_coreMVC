using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Depute
{
    public int DeputeId { get; set; }

    public int ProviderId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime Modifiedate { get; set; }

    public string? DeputeContent { get; set; }

    public decimal Salary { get; set; }

    public int StatusId { get; set; }

    public int RegionId { get; set; }

    public string? Title { get; set; }

    public int ViewCount { get; set; }

    public virtual ICollection<DeputeAction> DeputeActions { get; set; } = new List<DeputeAction>();

    public virtual ICollection<DeputeRecord> DeputeRecords { get; set; } = new List<DeputeRecord>();

    public virtual ICollection<DeputeSkill> DeputeSkills { get; set; } = new List<DeputeSkill>();

    public virtual Member Provider { get; set; } = null!;

    public virtual Region Region { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
