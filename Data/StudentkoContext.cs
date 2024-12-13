namespace Studentko.Data;
using Studentko.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

public class StudentkoContext : IdentityDbContext<ApplicationUser>
{

    public StudentkoContext(DbContextOptions<StudentkoContext> options) : base(options) { }

    //public required DbSet<ApplicationUser> User { get; set; }
    public DbSet<Event> Events { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Comment> Comments { get; set;}
    public DbSet<EventCategory> EventCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        modelBuilder.Entity<Event>().ToTable("Event");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<EventCategory>().ToTable("EventCategory");

    }

}
