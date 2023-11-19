using prjDB_GamingForm_Show.Models;
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

        public string id { get; set; }
        [DisplayName("懸賞來源")]
        public string providername { get; set; }
        [DisplayName("懸賞開始時間")]
        public string startdate { get; set; }

        [DisplayName("懸賞更新時間")]
        public string modifieddate { get; set; }

        [DisplayName("懸賞內容")]
        public string content { get; set; }

        [DisplayName("懸賞金額")]
        public string salary { get; set; }
        [DisplayName("懸賞狀態")]
        public string status { get; set; }
        [DisplayName("懸賞區域")]
        public string region { get; set; }

        public string Skillname { get; set; }
        public int Count { get; set; }
        




    }


}  
    

