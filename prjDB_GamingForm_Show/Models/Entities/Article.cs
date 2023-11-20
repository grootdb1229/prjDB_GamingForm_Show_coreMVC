using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public int SubBlogId { get; set; }

    [DisplayName("文章標題")]
    public string Title { get; set; } = null!;
    [DisplayName("文章內容")]
    public string ArticleContent { get; set; } = null!;
    [DisplayName("最後發文時間")]
    public DateTime ModifiedDate { get; set; }

    public int MemberId { get; set; }

    public int? ReplyArticleId { get; set; }

    public virtual ICollection<ArticleAction> ArticleActions { get; set; } = new List<ArticleAction>();

    public virtual ICollection<Article> InverseReplyArticle { get; set; } = new List<Article>();

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual Article? ReplyArticle { get; set; }

    public virtual SubBlog SubBlog { get; set; } = null!;
}
