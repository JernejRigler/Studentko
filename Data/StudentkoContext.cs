namespace Studentko.Data;
using Studentko.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

public class StudentkoContext : IdentityDbContext<ApplicationUser>
{

    public StudentkoContext(DbContextOptions<StudentkoContext> options) : base(options) { }

    //public required DbSet<ApplicationUser> User { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("User");

    }

}