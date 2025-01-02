using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Studentko.Controllers;

public class EventController : Controller
{
    private readonly StudentkoContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public EventController(StudentkoContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult FormEvent()
    {
        Console.WriteLine("Na dogdoek forum");
        return View();
    }
    public async Task<IActionResult> EventDetails(int id)
    {
        var currevent = await _context.Event
            .Where(p => p.PostID == id)
            .FirstOrDefaultAsync();
        if (currevent == null)
        {
            Console.WriteLine("Iskan dogodek ni bil najden");
            return RedirectToAction("Index", "Home");
        }
        return View(currevent);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PublishEvent(Event newEvent, IFormFile PostAttachment)
    {
        if (ModelState.IsValid)
        {
            if (PostAttachment != null)
            {
                //nek file hanadling
            }
            newEvent.type = "Dogodek";
            newEvent.createdAt = DateTime.Now;
            _context.Event.Add(newEvent);

            await _context.SaveChangesAsync();

            Console.WriteLine("dogodek je bila uspešno objavlena");
            return RedirectToAction("Index", "Home");
        }
        //errors
        foreach (var error in ModelState)
        {
            Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
        }
        return View("FormEvent");

    }
    [HttpPost]
    public async Task<IActionResult> JoinEvent(int eventID)
    {
        var currevent = await _context.Event
            .Where(p => p.PostID == eventID)
            .FirstOrDefaultAsync();
        var curruser = await _userManager.GetUserAsync(User);
        if (currevent == null || curruser == null)
        {
            Console.WriteLine("Iskani dogodek/user ni bil najden");
            return RedirectToAction("Index", "Home");
        }
        UserEvent newUserEvent = new UserEvent
        {
            UserID = curruser.Id,
            EventID = eventID,
            JoineddAt = DateTime.Now
        };
        _context.UserEvent.Add(newUserEvent);
        await _context.SaveChangesAsync();
        Console.WriteLine("uspesno prijavlen na dogodek");
        return RedirectToAction("EventDetails", "Event", new { id = eventID });
    }
}