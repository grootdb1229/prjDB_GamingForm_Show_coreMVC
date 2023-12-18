﻿using System;
using System.Collections.Generic;

namespace prjDB_GamingForm_Show.Models.Entities;

public partial class Article
{
    public int ArticleId { get; set; }

    public int SubBlogId { get; set; }

    public string Title { get; set; } = null!;

    public string ArticleContent { get; set; } = null!;

    public DateTime ModifiedDate { get; set; }

    public int MemberId { get; set; }

    public int? ReplyArticleId { get; set; }

    public int ViewCount { get; set; }

    public bool IsPinned { get; set; }

    public virtual ICollection<ArticleAction> ArticleActions { get; set; } = new List<ArticleAction>();

    public virtual ICollection<ArticleComplain> ArticleComplains { get; set; } = new List<ArticleComplain>();

    public virtual ICollection<Article> InverseReplyArticle { get; set; } = new List<Article>();

    public virtual Member Member { get; set; } = null!;

    public virtual ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public virtual Article? ReplyArticle { get; set; }

    public virtual SubBlog SubBlog { get; set; } = null!;

    public bool IsPinned { get; set; }  //1217
}
