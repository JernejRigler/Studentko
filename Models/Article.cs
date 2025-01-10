namespace Studentko.Models;
using System;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema;

public class Article : Post
{
    public string? subtitle { get; set; }

    public int ArticleCategoryID { get; set; }

    public bool IsTrending { get; set; }

    // Navigation Property
    public ArticleCategory? ArticleCategory { get; set; }

}