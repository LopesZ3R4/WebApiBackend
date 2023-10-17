// File Path: ./Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Alert> Alerts { get; set; }
    public DbSet<Machine> Machines { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Encaminhamento> Encaminhamento { get; set; }
}