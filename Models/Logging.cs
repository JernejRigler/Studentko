namespace Studentko.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Logging
{
    [Key]
    public int LoggingId { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }

    public virtual ApplicationUser User { get; set; }

    [Required]
    [StringLength(100)]
    public string Action { get; set; }

    [Required]
    public DateTime ActionTimestamp { get; set; }
}