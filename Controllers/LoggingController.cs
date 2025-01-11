using Studentko.Data;
using Studentko.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Studentko.Controllers;

public class LoggingController : Controller
{
    private readonly StudentkoContext _context;
    private readonly LoggingService _loggingService;

    public LoggingController(StudentkoContext context, LoggingService loggingService)
    {
        _context = context;
        _loggingService = loggingService;
    }

    // GET: Logs
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult Logging()
    {
        var logs = _context.Logs
                .Include(log => log.User)  // Include related User data (assuming it's a navigation property)
                .OrderByDescending(log => log.ActionTimestamp)  // Order by Timestamp descending
                .ToList();

        return View(logs);  // Pass logs to the view
    }
}
