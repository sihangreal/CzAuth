using CzAuth.Entities;
using Microsoft.EntityFrameworkCore;

namespace CzAuth.DbContexts
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public UserContext(DbContextOptions<UserContext> options) : base(options) { }
    }
}
