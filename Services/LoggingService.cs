namespace Studentko.Services;

using Studentko.Data;
using Studentko.Models;
public class LoggingService
{
    private readonly StudentkoContext _context;

    public LoggingService(StudentkoContext context)
    {
        _context = context;
    }

    public async Task LogActionAsync(string userId, string action)
    {
        var log = new Logging
        {
            UserId = userId,
            Action = action,
            ActionTimestamp = DateTime.UtcNow
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }
}