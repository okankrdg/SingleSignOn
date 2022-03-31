using Microsoft.EntityFrameworkCore;
using SingleSignOn.Entities;

namespace SingleSignOn;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> context)
        : base(context) { }
    public DbSet<User> Users { get; set; }
}

