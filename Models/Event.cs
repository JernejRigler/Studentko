namespace Studentko.Models;
using System;
using System.Collections.Generic;
using NuGet.Protocol.Plugins;

public class Event
{
    public int EventID { get; set;}
    public string? title { get; set; }
    public int? numberSpaces { get; set; }
    public string? description { get; set; }
    public bool? isTrending { get; set; } 

}
