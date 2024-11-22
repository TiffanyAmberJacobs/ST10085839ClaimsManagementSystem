using Microsoft.EntityFrameworkCore;

public class ClaimsDbContext : DbContext
{
    public DbSet<Claim> Claims { get; set; }

    public ClaimsDbContext(DbContextOptions<ClaimsDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Claim>()
            .Property(c => c.HourlyRate)
            .HasColumnType("decimal(18, 2)"); // 18 is the precision, 2 is the scale
    }
}

