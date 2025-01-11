using Microsoft.AspNetCore.Mvc;
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

    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> ViewCalendar()
    {
        var UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var userEvents = await _context.UserEvent.
                        Where(ue => ue.UserID == UserID).
                        Select(ue => ue.Event).ToListAsync();

        return View(userEvents);
    }

}