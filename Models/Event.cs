namespace Studentko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class Event : Post
{

    public int? ParticipantLimit { get; set; }
    public DateTime? EventDate { get; set; }
    public ICollection<UserEvent> Participants { get; set; } = new List<UserEvent>();
}
