using prjDB_GamingForm_Show.Models.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjDB_GamingForm_Show.Models.Admin
{
    public class CDeputeComplainsWrap
    {

        [DisplayName("編號")]
        public int Id { get; set; }
        [DisplayName("委託ID")]
        public int DeputeId { get; set; }
        [DisplayName("被申訴人ID")]
        public int ProviderId { get; set; }
        [DisplayName("申訴人ID")]
        public int MemberId { get; set; }
        [DisplayName("申訴理由")]
        public string SubTagId { get; set; }
        [DisplayName("申訴內容")]
        public string? ReportContent { get; set; }
        [DisplayName("申訴時間")]
        public DateTime ReportDate { get; set; }
        [DisplayName("申訴狀態")]
        public string? Status { get; set; }

    }
}
