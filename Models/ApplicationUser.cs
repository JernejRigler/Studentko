using Microsoft.AspNetCore.Identity;
namespace Studentko.Models;
public class ApplicationUser : IdentityUser
{
    public ICollection<UserEvent> UserEvents { get; set;} = new List<UserEvent>();
}