using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;

namespace Studentko.Controllers;

public class EventController : Controller
{
    private readonly StudentkoContext _context;
    public EventController(StudentkoContext context){
        _context = context;
    }
     [HttpGet]
    public IActionResult FormEvent(){
        Console.WriteLine("Na dogdoek forum");
        return View();
    }
    public async Task<IActionResult> EventDetails(int id){
        var currevent = await _context.Event
            .Where(p => p.PostID == id)
            .FirstOrDefaultAsync();
        if(currevent == null){
            Console.WriteLine("Iskan dogodek ni bil najden");
            return RedirectToAction("Index", "Home");
        }
        return View(currevent);
    }
    [HttpPost]
    public async Task<IActionResult> PublishEvent(Event newEvent, IFormFile PostAttachment){
        if(ModelState.IsValid){
            if(PostAttachment != null){
                //nek file hanadling
            }
            newEvent.type = "Dogodek";
            newEvent.createdAt = DateTime.Now;
            _context.Event.Add(newEvent);

            await _context.SaveChangesAsync();

            Console.WriteLine("dogodek je bila uspešno objavlena");
            return RedirectToAction("Index","Home");
        }
        //errors
        foreach (var error in ModelState)
        {
        Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
        }
        return View("FormEvent");
        
    }
}