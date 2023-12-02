using prjDB_GamingForm_Show.Models;
using prjDB_GamingForm_Show.Models.Entities;
using prjDB_GamingForm_Show.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DB_GamingForm_Show.Job.DeputeClass
{
    public class CDeputeViewModel
    {

        public int id { get; set; }
        [DisplayName("懸賞來源")]
        public string title { get; set; }
        [DisplayName("懸賞開始時間")]
        public string providername { get; set; }
        [DisplayName("懸賞開始時間")]
        public string startdate { get; set; }

        [DisplayName("懸賞更新時間")]
        public string modifieddate { get; set; }

        [DisplayName("懸賞內容")]
        public string deputeContent { get; set; }

        [DisplayName("懸賞金額")]
        public decimal salary { get; set; }
        [DisplayName("懸賞狀態")]
        public string statusid {  get; set; }
        public string status { get; set; }
        [DisplayName("懸賞區域")]
        public string region { get; set; }
        [DisplayName("懸賞技能")]
        public string skillname { get; set; }
        public int skillid { get; set; }
        [DisplayName("懸賞金主")]
        public string memeberContent { get; set; }
        public string memberName {  get; set; }
        public string imgfilepath { get; set; }
        public int count { get; set; }

        public string? skilllist { get; set; }
        public string? applyerPhone { get; set; }
        public string? applyerBirth { get; set; }
        public string? applyerEmail { get; set; }
        public string? applyerContent { get; set; }
        public string? applyerGender {  get; set; }

    }


}  
    

