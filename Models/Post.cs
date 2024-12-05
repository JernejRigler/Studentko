namespace Studentko.Models;
using System;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema; 

public class Post
{
    public int PostID { get; set;}
    public string? title { get; set; }
    public string? subtitle { get; set; }
    public string? content { get; set; }

    [NotMapped]
    public IFormFile? PostAttachment { get; set; }

    public string? FileAttachment {get; set;}

    public DateTime? date  { get; set; }
    public bool? isTrending { get; set; } 

}
