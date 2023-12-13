using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace prjDB_GamingForm_Show.Models.Member
{
    public class CEmail
    {
        public string ToEmail { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public List<string> Emails { get;set; }
        public View EmailView { get; set; }
    }
}
