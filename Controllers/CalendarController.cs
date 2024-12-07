using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Studentko.Data;


namespace Studentko.Controllers;

public class CalendarController : Controller
{
    public IActionResult ViewCalendar()
    {
        Console.WriteLine("Halo želiš videti koledar jaaja");
        return View();
    }

}