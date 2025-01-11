using Microsoft.AspNetCore.Mvc;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Studentko.Services;


namespace Studentko.Controllers;

public class CommentController : Controller
{
    private readonly StudentkoContext _context;
    private readonly LoggingService _loggingService;
    public CommentController(StudentkoContext context, LoggingService loggingService)
    {
        _context = context;
        _loggingService = loggingService;
    }
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddComment(Comment newComment)
    {
        if (ModelState.IsValid)
        {
            newComment.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Comment.Add(newComment);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == newComment.UserID);
            if (user != null)
            {
                newComment.user = user;
                await _loggingService.LogActionAsync(newComment.user.Id, "Dodan komentar");
            }

            return RedirectToAction("Index", "Home", new { id = newComment.PostID });
        }
        return RedirectToAction("Index", "Home", new { id = newComment.PostID });
    }
}