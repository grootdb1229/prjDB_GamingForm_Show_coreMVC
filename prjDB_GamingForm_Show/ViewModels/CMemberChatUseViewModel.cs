namespace prjDB_GamingForm_Show.ViewModels
{
    public class CMemberChatUseViewModel
    {
        public string? SenderName { get; set; }
        public string? ReceiverName { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? SenderImg { get; set; }
        public string? ReceiverImg { get; set; }
        public string? ChatContent { get; set; }
        public string? ModefiedDate { get; set; }
        public bool? IsCheck { get; set; }
    }
}
