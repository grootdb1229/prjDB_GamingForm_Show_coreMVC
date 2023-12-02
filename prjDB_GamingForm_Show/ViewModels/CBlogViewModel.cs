using prjDB_GamingForm_Show.Models.Entities;

namespace prjDB_GamingForm_Show.ViewModels
{
    public class CBlogViewModel
    {
        public IEnumerable<Blog>? blogs { get; set; }
        public IEnumerable<Article>? articles { get; set; }
        public IEnumerable<SubBlog>? subBlogs { get; set; }
        public IEnumerable<SubTag>? subTags { get; set; }
        public IEnumerable<Tag>? tags { get; set; }
        public IEnumerable<Models.Entities.Action>? actions { get; set; }
        public IEnumerable<ArticleAction>? articleActions { get; set; }
        public IEnumerable<Reply>? replies { get; set; }
        public IEnumerable<Member>? members { get; set; }

        public IEnumerable<Image>? Images { get; set; }

       

    }
}
