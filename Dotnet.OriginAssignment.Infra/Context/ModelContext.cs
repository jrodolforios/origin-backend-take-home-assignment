using Dotnet.OriginAssignment.Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace Dotnet.OriginAssignment.Infra.Context;

public partial class ModelContext : DbContext
{
    public ModelContext(DbContextOptions<ModelContext> options) : base(options)
    {
    }

    public DbSet<EligibilityFile> EligibilityFiles { get; set; }
    public DbSet<ProcessedLine> ProcessedLines { get; set; }
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ProcessedLine>()
            .HasOne(pl => pl.EligibilityFile)
            .WithMany()
            .HasForeignKey(pl => pl.EligibilityFileId);

        modelBuilder.Entity<Report>()
            .HasOne(r => r.EligibilityFile)
            .WithMany()
            .HasForeignKey(r => r.EligibilityFileId);

        modelBuilder.Entity<Report>()
            .HasMany(r => r.ProcessedLines)
            .WithOne()
            .HasForeignKey(pl => pl.EligibilityFileId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Report>()
            .HasMany(r => r.UnprocessedLines)
            .WithOne()
            .HasForeignKey(pl => pl.EligibilityFileId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
