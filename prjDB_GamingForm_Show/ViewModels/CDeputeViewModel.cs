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
        [DisplayName("委託編號")]
        public int id { get; set; }
        [DisplayName("委託標題")]
        public string title { get; set; }
        [DisplayName("會員名稱")]
        public string providername { get; set; }
        [DisplayName("開始時間")]
        public string startdate { get; set; }
        [DisplayName("最後更新")]
        public string modifieddate { get; set; }
        [DisplayName("委託內容")]
        public string deputeContent { get; set; }
        
        public decimal salary { get; set; }
        public int statusid {  get; set; }
        [DisplayName("懸賞狀態")]
        public string status { get; set; }
        
        public string region { get; set; }
        [DisplayName("懸賞技能")]
        public string skillname { get; set; }
        public int skillid { get; set; }
        public string listskillid { get; set; }
        public string listskillclassid { get; set; }
        [DisplayName("懸賞金主")]
        public int viewcount { get; set; }
        public string memeberContent { get; set; }
        public string workerName {  get; set; }
        public string imgfilepath { get; set; }
        
        public int count { get; set; }
        public int workerId {  get; set; }

        public string? skilllist { get; set; }
        public string? applyerPhone { get; set; }
        public string? applyerBirth { get; set; }
        public string? applyerEmail { get; set; }
        public string? applyerContent { get; set; }
        public string? applyerGender {  get; set; }
        public string? replyContent {  get; set; }
        public string? replyFileName {  get; set; }
    }


}  
    

