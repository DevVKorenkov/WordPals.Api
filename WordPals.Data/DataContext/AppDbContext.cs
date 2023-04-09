using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WordPals.Models.Models;

namespace WordPals.Data.DataContext;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<WordModel> Words { get; set; }
    public DbSet<Vocabulary> Vocabulary { get; set; }
    public DbSet<Definition> Definitions { get; set; }
    public DbSet<AppIdentityUser> AppUsers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<AppIdentityUser>(entity =>
        {
            entity
                .HasMany(u => u.FavoriteWords)
                .WithMany(v => v.UsersWhoHaveWord)
                .UsingEntity(j => j.ToTable("FavoriteWords"));
        });
        
        builder.Entity<Vocabulary>(entity =>
        {
            entity.HasOne(v => v.Owner);
            entity.HasOne(v => v.FromWhom);
        });
    }
}
