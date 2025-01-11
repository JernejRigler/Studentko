using Microsoft.AspNetCore.Mvc;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Studentko.Services;
using System.Security.Claims;


namespace Studentko.Controllers;

public class ArticleController : Controller
{
    private readonly StudentkoContext _context;
    private readonly LoggingService _loggingService;
    public ArticleController(StudentkoContext context, LoggingService loggingService)
    {
        _context = context;
        _loggingService = loggingService;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult FormArticle()
    {
        var newPost = new Article { };
        var categories = _context.ArticleCategories.ToList();
        ViewData["ArticleCategories"] = categories;
        return View(newPost);
    }
    public async Task<IActionResult> ArticleDetails(int id)
    {
        var article = await _context.Article
            .Where(p => p.PostID == id)
            .Include(p => p.Comments)
                .ThenInclude(c => c.user)
            .FirstOrDefaultAsync();
        if (article == null)
        {
            return RedirectToAction("Index", "Home");
        }
        return View(article);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PublishArticle(Article newArticle)
    {
        if (ModelState.IsValid)
        {
            newArticle.type = "Članek";
            newArticle.createdAt = DateTime.Now;
            _context.Article.Add(newArticle);

            await _context.SaveChangesAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                await _loggingService.LogActionAsync(userId, "Članek dodan");
            }

            return RedirectToAction("Index", "Home");
        }
        //errors
        foreach (var error in ModelState)
        {
            Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
        }
        return View("FormArticle");

    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ArticleDelete(int id)
    {
        if (ModelState.IsValid)
        {

            var post = await _context.Article.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            _context.Article.Remove(post);
            await _context.SaveChangesAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                await _loggingService.LogActionAsync(userId, "Članek izbrisan");
            }
        }
        else
        {
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
        }
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ArticleEdit(int id)
    {
        var post = await _context.Article.FindAsync(id);
        var categories = _context.ArticleCategories.ToList();
        ViewData["ArticleCategories"] = categories;
        if (post == null)
        {
            return NotFound();
        }
        return View("FormArticle", post);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ArticleEdit(Article updatedArticle)
    {
        if (!ModelState.IsValid)
        {
            // Reuse the "CreatePost" view to show validation errors
            return View("FormArticle", updatedArticle);
        }
        var post = await _context.Article.FindAsync(updatedArticle.PostID);
        if (post == null)
        {
            return NotFound();
        }

        post.title = updatedArticle.title;
        post.subtitle = updatedArticle.subtitle;
        post.content = updatedArticle.content;
        post.ArticleCategoryID = updatedArticle.ArticleCategoryID;
        post.IsTrending = updatedArticle.IsTrending;

        await _context.SaveChangesAsync();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            await _loggingService.LogActionAsync(userId, "Članek spremenjen");
        }

        return RedirectToAction("Index", "Home");
    }
}