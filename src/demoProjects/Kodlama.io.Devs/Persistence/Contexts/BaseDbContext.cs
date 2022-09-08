using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Persistence.Contexts;

public class BaseDbContext : DbContext
{
    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected IConfiguration Configuration { get; set; }
    public DbSet<ProgrammingLanguage> ProgrammingLanguages { get; set; }
    public DbSet<Technology> Technologies { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(
        //     optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DevConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(a =>
        {
            a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");

            a.HasMany(p => p.Technologies);
        });

        modelBuilder.Entity<Technology>(a =>
        {
            a.ToTable("Technologies").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.ProgrammingLanguageId).HasColumnName("ProgrammingLanguageId");
            a.Property(p => p.Name).HasColumnName("Name");

            a.HasOne(p => p.ProgrammingLanguage);
        });

        ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "Java"), new(2, "C#") };

        Technology[] technologiesEntitySeeds =
            { new(1, 1, "Spring"), new(2, 2, "ASP.NET"), new(3, 1, "JSP") };

        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);
        modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);
    }
}