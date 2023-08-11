using Microsoft.EntityFrameworkCore;

namespace tetris_api;

public class User
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string? Username { get; set; }

    public string? HashedPassword { get; set; }
}

public class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Score> Scores => Set<Score>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseIdentityAlwaysColumns();
    }
}

public class UsernameAndPassword
{
    public string? Username { get; set; }

    public string? HashedPassword { get; set; }
}
