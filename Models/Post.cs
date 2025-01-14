namespace Studentko.Models;
using System;
using System.Collections.Generic;

public abstract class Post
{
   public int PostID { get; set; }
   public string? title { get; set; }

   public string? content { get; set; }

   public DateTime? createdAt { get; set; }

   public string? type { get; set; }

   public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
