namespace Studentko.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Logging
{
    [Key]
    public int LoggingId { get; set; } // Primary Key

    [Required]
    [ForeignKey("User")] // Foreign key to the User table
    public string UserId { get; set; } // Assuming you're using Identity with string UserId

    public virtual ApplicationUser User { get; set; } // Navigation property

    [Required]
    [StringLength(100)]
    public string Action { get; set; } // Description of the action (e.g., "Added Post")

    [Required]
    public DateTime ActionTimestamp { get; set; } // Timestamp of the action
}