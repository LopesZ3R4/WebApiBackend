// File Path: ./Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Alert> Alerts { get; set; }
    public DbSet<Link> Links { get; set; }
    public DbSet<Value> Values { get; set; }
    public DbSet<Duration> Durations { get; set; }
    public DbSet<EngineHours> EngineHours { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Definition> Definitions { get; set; }
    public DbSet<DefinitionLink> DefinitionLinks { get; set; }
    public DbSet<AlertLink> AlertLinks { get; set; }
}