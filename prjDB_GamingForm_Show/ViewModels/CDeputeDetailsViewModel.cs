using System.ComponentModel;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CDeputeDetailsViewModel
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
        [DisplayName("委託價格")]
        public decimal salary { get; set; }
        [DisplayName("委託狀態")]
        public string status { get; set; }
        [DisplayName("委託區域")]
        public string region { get; set; }
        [DisplayName("委託人自屆")]
        public string memeberContent { get; set; }
        [DisplayName("委託人相片")]
        public string imgfilepath { get; set; }
    }
}
