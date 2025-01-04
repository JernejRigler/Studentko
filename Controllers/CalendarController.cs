using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Studentko.Models;
using Studentko.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Studentko.Controllers;

public class CalendarController : Controller
{
    private readonly StudentkoContext _context;
     public CalendarController(StudentkoContext context)
    {
        _context = context;
    }

    public async  Task<IActionResult> ViewCalendar()
    {
        var eventDates = await _context.Event.ToListAsync();

        Console.WriteLine("Halo želiš videti koledar jaaja");
        return View(eventDates);
    }

}