using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;


namespace Studentko.Controllers;

public class CommentController : Controller
{
    private readonly StudentkoContext _context;
    public CommentController(StudentkoContext context)
    {
        _context = context;
    }
    [HttpPost]
    public async Task<IActionResult> AddComment(Comment newComment){
        if(ModelState.IsValid){
             newComment.UserID =  User.FindFirstValue(ClaimTypes.NameIdentifier);

             _context.Comments.Add(newComment);
             await _context.SaveChangesAsync();

            return RedirectToAction("PostDetails","Post", new { id = newComment.PostID});
        }
        return RedirectToAction("PostDetails","Post", new { id = newComment.PostID});
    }
}