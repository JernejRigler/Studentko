using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;

namespace Studentko.Controllers;

public class UserController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    [HttpGet]
    public IActionResult Register()
    {
        Console.WriteLine("registracija");
        return View();
    }
    [HttpGet]
    public IActionResult PublishPost(){
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string username, string pwd)
    {
        var result = await _signInManager.PasswordSignInAsync(
            username,
            pwd,
            isPersistent: false,
            lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return RedirectToAction("Index", "Home");
        }

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string pwd, string confirmpwd)
    {
        var user = new ApplicationUser { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, pwd);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            Console.WriteLine("succesful register!");
            return RedirectToAction("Index", "Home");
        }
        foreach (var error in result.Errors)
        {
            Console.WriteLine("Neki je slo narobe");
            Console.WriteLine(error + ":" + error.Description);
            ModelState.AddModelError(string.Empty, error.Description);

        }
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
    [HttpPost]
    public IActionResult PublishPost(string title, string subtitle, string postcontent, IFormFile postAttachment){

        Console.WriteLine("objava je bila uspešno objavlena");
        return RedirectToAction("Index","Home");
    }

    
}
