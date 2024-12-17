namespace Studentko.Data;
using Studentko.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

public class StudentkoContext : IdentityDbContext<ApplicationUser>
{

    public StudentkoContext(DbContextOptions<StudentkoContext> options) : base(options) { }

    //public required DbSet<ApplicationUser> User { get; set; }
    public DbSet<Post> Post { get; set; }
    public DbSet<Event> Event { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Comment> Comment { get; set;}
    public DbSet<EventCategory> EventCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Article>().ToTable("Article");
        modelBuilder.Entity<Event>().ToTable("Event");
        modelBuilder.Entity<Comment>().ToTable("Comment");
        modelBuilder.Entity<EventCategory>().ToTable("EventCategory");

    }

}
