namespace Studentko.Models;
using System;
public class UserEvent
{
    public string UserID { get; set; }
    public ApplicationUser User { get; set; }
    public int EventID { get; set; }
    public Event Event { get; set; }

    public DateTime JoineddAt { get; set; }
}