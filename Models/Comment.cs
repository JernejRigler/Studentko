namespace Studentko.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public class Comment
{
    public int CommentID { get; set; }
    public string? content { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;


    [ForeignKey("Post")]
    public int PostID { get; set; }
    public Post? posts { get; set; }

    [ForeignKey("ApplicationUser")]
    public string? UserID { get; set; }
    public ApplicationUser? user { get; set; }
}