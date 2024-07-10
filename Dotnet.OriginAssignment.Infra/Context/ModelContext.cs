using Dotnet.OriginAssignment.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.OriginAssignment.Infra.Context;

public partial class ModelContext : DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Engine> Engines { get; set; }
    public DbSet<Wheel> Wheels { get; set; }
    public DbSet<Interior> Interiors { get; set; }

    public ModelContext(DbContextOptions<ModelContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define the relationship between Car and Engine
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Engine)
            .WithMany()
            .HasForeignKey(c => c.EngineId);

        // Define the relationship between Car and Interior
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Interior)
            .WithMany()
            .HasForeignKey(c => c.InteriorId);

        // Define the relationship between Car and Wheel
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Wheels)
            .WithOne(w => w.Car)
            .HasForeignKey(w => w.CarId);
    }
}
