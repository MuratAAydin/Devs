using Core.Security.Entities;
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
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<GitHubConnection> GitHubConnections { get; set; }

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

        modelBuilder.Entity<User>(a =>
        {
            a.ToTable("Users").HasKey(k => k.Id);
            a.Property(p => p.FirstName).HasColumnName("FirstName");
            a.Property(p => p.LastName).HasColumnName("LastName");
            a.Property(p => p.Email).HasColumnName("Email");
            a.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
            a.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
            a.Property(p => p.Status).HasColumnName("Status");
            a.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");
        });

        modelBuilder.Entity<OperationClaim>(a =>
        {
            a.ToTable("OperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.Name).HasColumnName("Name");
        });

        modelBuilder.Entity<UserOperationClaim>(a =>
        {
            a.ToTable("UserOperationClaims").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");

            a.HasOne(p => p.User);
            a.HasOne(p => p.OperationClaim);
        });

        modelBuilder.Entity<GitHubConnection>(a =>
        {
            a.ToTable("GitHubConnections").HasKey(k => k.Id);
            a.Property(p => p.Id).HasColumnName("Id");
            a.Property(p => p.UserId).HasColumnName("UserId");
            a.Property(p => p.Name).HasColumnName("Name");

            a.HasOne(p => p.User);
        });

        ProgrammingLanguage[] programmingLanguageEntitySeeds = { new(1, "Java"), new(2, "C#") };

        Technology[] technologiesEntitySeeds =
            { new(1, 1, "Spring"), new(2, 2, "ASP.NET"), new(3, 1, "JSP") };

        GitHubConnection[] gitHubConnectionEntitySeeds = { new(1, 1, "github.com") };

        modelBuilder.Entity<ProgrammingLanguage>().HasData(programmingLanguageEntitySeeds);
        modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);
        modelBuilder.Entity<GitHubConnection>().HasData(gitHubConnectionEntitySeeds);
    }
}