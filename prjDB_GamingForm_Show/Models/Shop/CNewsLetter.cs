using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class CNewsLetter
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string HtmlContent { get; set; }
        public List<string> Emails { get; set; }
        public View EmailView { get; set; }
    }
}
