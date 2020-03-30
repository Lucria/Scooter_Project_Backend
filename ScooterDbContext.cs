using Microsoft.EntityFrameworkCore;

public class ScooterDbContext : DbContext
{
    public ScooterDbContext(DbContextOptions<ScooterDbContext> options) : base(options)
    {
        
    }
}