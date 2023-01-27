using Lab5.model;

namespace Lab5.repository;

using Microsoft.EntityFrameworkCore;

public class AttemptContext : DbContext
{
    public DbSet<Attempt> Attempts => Set<Attempt>();

    public AttemptContext()
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=postgres;Database=postgres");
        base.OnConfiguring(optionsBuilder);
    }
    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Attempt>(entity =>
    //     {
    //         // if (!Database.IsInMemory()) return;
    //         entity.Property(p => p.ContendersRates)
    //             .HasConversion(
    //                 v => string.Join(" ", v),  
    //                 v => Array.ConvertAll(
    //                     v.Split(' ', StringSplitOptions.RemoveEmptyEntries), int.Parse));
    //                     
    //         entity.Ignore(p => p.ContendersNames);
    //     });
    //     base.OnModelCreating(modelBuilder);
    // }
}