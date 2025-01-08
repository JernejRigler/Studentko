namespace Studentko.Models;
using System;
using System.Collections.Generic;
public class ArticleCategory
{
    public int ArticleCategoryID { get; set; }
    public string category { get; set; }
    public ICollection<Article> Articles { get; set; }

}