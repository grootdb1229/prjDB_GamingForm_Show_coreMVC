using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CProductComplainViewModel
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string ReplyContent { get; set; }

        public int MemeberId { get; set; }

        public string ReportDate { get; set; } = null!;
        public IEnumerable<ProductComplain>? ProductComplain { get; set; }
        public IEnumerable<Member>? Member { get; set; } = null!;

        public IEnumerable<Product>? Product { get; set; } = null!;
    }
}
