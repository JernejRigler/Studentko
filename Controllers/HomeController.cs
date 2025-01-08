using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentko.Data;
using Studentko.Models;

namespace Studentko.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly StudentkoContext _context;

    public HomeController(ILogger<HomeController> logger, StudentkoContext context)
    {
        _logger = logger;
        _context = context;
    }

    /*
    public IActionResult Index()
    {
        return View();
    }
    */
    public async Task<IActionResult> Index(int? categoryId)
    {
        var articlesQuery = _context.Article.Include(a => a.ArticleCategory).AsQueryable();

        if (categoryId.HasValue)
        {
            articlesQuery = articlesQuery.Where(a => a.ArticleCategoryID == categoryId.Value);
        }

        var articles = await articlesQuery.ToListAsync();

        var events = await _context.Event.Include(e => e.Participants).ToListAsync();

        var articleCategories = await _context.ArticleCategories.ToListAsync();

        ViewData["ArticleCategories"] = articleCategories;
        ViewData["SelectedCategoryId"] = categoryId;

        var posts = new List<Post>();
        posts.AddRange(articles);
        posts.AddRange(events);

        posts = posts.OrderByDescending(item => item.createdAt).ToList();

        return View(posts);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
