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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
            base.OnConfiguring(
                optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DevConnectionString")));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgrammingLanguage>(a =>
        {
            a.ToTable("ProgrammingLanguages").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");
        });


        ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "Java"), new(2, "C#") };
        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);
    }
}