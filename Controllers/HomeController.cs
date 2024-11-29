using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Studentko.Models;

namespace Studentko.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    public IActionResult Login(){
        
        return View();
    }
    //ne vem točno zakaj more bit to tle in ne v StudentController, kjer bi blo simselno, samo če je tuki dela, če pa dam tja pa ne
    //mogoče neki z asp-controller ?? 
     public IActionResult Register(){
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
