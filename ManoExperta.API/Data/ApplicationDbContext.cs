using ManoExperta.API.Domain;
using Microsoft.EntityFrameworkCore;

namespace ManoExperta.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<ProfessionalProfile> ProfessionalProfiles { get; set; }
    public DbSet<ProfessionalCategory> ProfessionalCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ProfessionalCategory>()
            .HasIndex(c => c.Code)
            .IsUnique();

        builder.Entity<User>()
            .HasIndex(u => u.UserName)
            .IsUnique();
    }
}
