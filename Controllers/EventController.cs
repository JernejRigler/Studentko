using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Studentko.Services;

namespace Studentko.Controllers;

public class EventController : Controller
{
    private readonly StudentkoContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly LoggingService _loggingService;
    public EventController(StudentkoContext context, UserManager<ApplicationUser> userManager, LoggingService loggingService)
    {
        _context = context;
        _userManager = userManager;
        _loggingService = loggingService;
    }
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult FormEvent()
    {
        var newPost = new Event { };
        return View(newPost);
    }
    public async Task<IActionResult> EventDetails(int id)
    {
        var currevent = await _context.Event
            .Where(p => p.PostID == id)
            .Include(e => e.Participants)
            .Include(p => p.Comments)
              .ThenInclude(c => c.user)
            .FirstOrDefaultAsync();


        if (currevent == null)
        {
            return RedirectToAction("Index", "Home");
        }
        var currentuser = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var isRegistered = await _context.UserEvent.AnyAsync(ue => ue.UserID == currentuser && ue.EventID == id);
        ViewBag.isRegistered = isRegistered;
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

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                await _loggingService.LogActionAsync(userId, "Dogodek dodan");
            }

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
        return RedirectToAction("EventDetails", "Event", new { id = eventID });
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EventDelete(int id)
    {
        if (ModelState.IsValid)
        {

            var currevent = await _context.Event.FindAsync(id);
            if (currevent == null)
            {
                return NotFound();
            }
            _context.Event.Remove(currevent);
            await _context.SaveChangesAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId != null)
            {
                await _loggingService.LogActionAsync(userId, "Dogodek izbrisan");
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
    public async Task<IActionResult> EventEdit(int id)
    {
        var post = await _context.Event.FindAsync(id);
        if (post == null)
        {
            return NotFound();
        }

        return View("FormEvent", post);
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> EventEdit(Event updatedEvent)
    {
        if (!ModelState.IsValid)
        {
            return View("FormArticle", updatedEvent);
        }
        var post = await _context.Event.FindAsync(updatedEvent.PostID);
        if (post == null)
        {
            return NotFound();
        }

        post.title = updatedEvent.title;
        post.content = updatedEvent.content;
        post.ParticipantLimit = updatedEvent.ParticipantLimit;
        post.EventDate = updatedEvent.EventDate;

        await _context.SaveChangesAsync();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            await _loggingService.LogActionAsync(userId, "Dogodek spremenjen");
        }

        return RedirectToAction("Index", "Home");
    }
}