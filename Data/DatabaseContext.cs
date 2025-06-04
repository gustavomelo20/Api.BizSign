using Microsoft.EntityFrameworkCore;
using Api.BizSign.Models;

namespace Api.BizSign.Data;


public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; init; }
    public virtual DbSet<Document> Documents { get; init; }
    public virtual DbSet<Signatory> Signatories { get; init; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>();
        modelBuilder.Entity<Document>();
        modelBuilder.Entity<Signatory>();
    }
}
