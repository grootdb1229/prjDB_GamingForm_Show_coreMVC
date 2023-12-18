using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace prjDB_GamingForm_Show.Models.Shop
{
    public class CNewsLetter
    {
        public int Id { get; set; }

        public int Title { get; set; }

        public int HtmlContent { get; set; }
        public List<string> Emails { get; set; }
        public View EmailView { get; set; }
    }
}
