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
        Console.WriteLine("Na članek forum");
        return View();
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
        if (ModelState.IsValid)
        {
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
        return View("FormPost");

    }
}