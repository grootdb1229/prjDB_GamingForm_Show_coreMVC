using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class JobCertificate
{
    public int Id { get; set; }

    public int CertificateId { get; set; }

    public int JobId { get; set; }
}
