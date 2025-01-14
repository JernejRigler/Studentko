namespace Studentko.Models;

public class Article : Post
{
    public string? subtitle { get; set; }

    public int ArticleCategoryID { get; set; }

    public bool IsTrending { get; set; }

    public ArticleCategory? ArticleCategory { get; set; }

}