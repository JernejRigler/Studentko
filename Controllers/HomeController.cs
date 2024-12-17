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
    public async Task<IActionResult> Index()
{
    var article = await _context.Article.ToListAsync();
    var events = await _context.Event.ToListAsync();

    var posts = new List<Post>();

    posts.AddRange(article);
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
