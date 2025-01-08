using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Studentko.Controllers;

public class ArticleController : Controller
{
    private readonly StudentkoContext _context;
    public ArticleController(StudentkoContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult FormArticle()
    {
        var newPost = new Article { };
        var categories = _context.ArticleCategories.ToList();
        ViewData["ArticleCategories"] = categories;
        Console.WriteLine("Na članek forum");
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
            Console.WriteLine("Iskana objava ni bila najdena");
            return RedirectToAction("Index", "Home");
        }
        return View(article);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PublishArticle(Article newArticle)
    {
        Console.WriteLine("imhere");
        Console.WriteLine(newArticle.ArticleCategoryID);
        if (ModelState.IsValid)
        {
            Console.WriteLine("imhere2");
            newArticle.type = "Članek";
            newArticle.createdAt = DateTime.Now;
            _context.Article.Add(newArticle);

            await _context.SaveChangesAsync();

            Console.WriteLine("objava je bila uspešno objavlena");
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
        Console.WriteLine("post za utofilat" + post.ArticleCategoryID);
        return View("FormArticle", post);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ArticleEdit(Article updatedArticle)
    {
        if (!ModelState.IsValid)
        {
            // Reuse the "CreatePost" view to show validation errors
            Console.WriteLine("invalid");
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

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }
}