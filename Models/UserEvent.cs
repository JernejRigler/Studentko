namespace Studentko.Models;
using System;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations.Schema; 

public class UserEvent
{
    public string UserID { get; set;}
    public ApplicationUser User { get; set; }
    public int EventID { get; set; }
    public Event Event { get; set;}

    public DateTime JoineddAt { get; set; }



   
}