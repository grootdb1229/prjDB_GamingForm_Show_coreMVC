using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CAdminBlogViewModel
    {
        public IFormFile Photo { get; set; }

        public int BlogId { get; set; }

        public string Title { get; set; } = null!;

        public int SubTagId { get; set; }

        public string? FImagePath { get; set; }


    }
}
