using Microsoft.EntityFrameworkCore;
using Vietravel.Tours.Domain;

namespace Vietravel.Tours.Infrastructure.Persistence;

public sealed class ToursDbContext(DbContextOptions<ToursDbContext> options) : DbContext(options)
{
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var tours = modelBuilder.Entity<Tour>();
        tours.ToTable("Tours");

        tours.HasKey(x => x.Id);
        tours.Property(x => x.Id).ValueGeneratedOnAdd();

        tours.Property(x => x.Name).IsRequired().HasMaxLength(200);
        tours.Property(x => x.City).IsRequired().HasMaxLength(100);
        tours.Property(x => x.Price).HasPrecision(18, 2);
        tours.Property(x => x.CreatedAt).IsRequired();

        tours.HasIndex(x => x.City).HasDatabaseName("IX_Tours_City");

        var users = modelBuilder.Entity<User>();
        users.ToTable("Users");

        users.HasKey(x => x.Id);
        users.Property(x => x.Id).ValueGeneratedOnAdd();

        users.Property(x => x.Username).IsRequired().HasMaxLength(100);
        users.Property(x => x.Password).IsRequired();
        users.Property(x => x.CreatedAt).IsRequired();

        users.HasIndex(x => x.Username).IsUnique().HasDatabaseName("UX_Users_Username");
    }
}

