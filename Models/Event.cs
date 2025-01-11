namespace Studentko.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

public class Event : Post
{

    public int? ParticipantLimit { get; set; }
    public DateTime? EventDate { get; set; } //datum za ko se bo dogodek začel

    public string? FileAttachment { get; set; }

    [NotMapped]
    public IFormFile? PostAttachment { get; set; }

    public ICollection<UserEvent> Participants { get; set; } = new List<UserEvent>();

}
