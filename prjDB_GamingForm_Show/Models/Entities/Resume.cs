using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Resume
{
    public int ResumeId { get; set; }

    public int MemberId { get; set; }

    public string FullName { get; set; } = null!;

    public string IdentityId { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string? ResumeContent { get; set; }

    public string? WorkExp { get; set; }

    public int FormId { get; set; }

    public int? ResumeStatusId { get; set; }

    public int? ImageId { get; set; }

    public int? Edid { get; set; }
}
