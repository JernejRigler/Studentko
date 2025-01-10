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

    public DbSet<UserEvent> UserEvent { get; set; }
    public DbSet<Article> Article { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<ArticleCategory> ArticleCategories { get; set; }
    public DbSet<Logging> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ApplicationUser>().ToTable("User");
        modelBuilder.Entity<Post>().ToTable("Post");
        modelBuilder.Entity<Article>().ToTable("Article");
        modelBuilder.Entity<Event>().ToTable("Event");
        modelBuilder.Entity<Comment>().ToTable("Comment");
        modelBuilder.Entity<ArticleCategory>().ToTable("ArticleCategory");

        modelBuilder.Entity<ArticleCategory>().HasData(
        new ArticleCategory { ArticleCategoryID = 1, category = "Obvestila" },
        new ArticleCategory { ArticleCategoryID = 2, category = "Predmetnik" },
        new ArticleCategory { ArticleCategoryID = 3, category = "Pomoč učenju" },
        new ArticleCategory { ArticleCategoryID = 4, category = "Šport" },
        new ArticleCategory { ArticleCategoryID = 5, category = "Zaposlitvene možnosti" },
        new ArticleCategory { ArticleCategoryID = 6, category = "Seminarji" }
    );
        modelBuilder.Entity<Article>()
        .HasOne(a => a.ArticleCategory)
        .WithMany(ac => ac.Articles)
        .HasForeignKey(a => a.ArticleCategoryID)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<UserEvent>().ToTable("UserEvent");
        modelBuilder.Entity<UserEvent>().HasKey(ueKey => new { ueKey.UserID, ueKey.EventID });
        modelBuilder.Entity<UserEvent>().HasOne(ueKey => ueKey.User).WithMany(userKey => userKey.UserEvents).HasForeignKey(ueKey => ueKey.UserID);
        modelBuilder.Entity<UserEvent>().HasOne(ueKey => ueKey.Event).WithMany(eventKey => eventKey.Participants).HasForeignKey(ueKey => ueKey.EventID);

        modelBuilder.Entity<Logging>()
            .HasOne(l => l.User)
            .WithMany(u => u.Logs)
            .HasForeignKey(l => l.UserId);

    }

}
