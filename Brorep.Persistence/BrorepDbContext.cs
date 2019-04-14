using Brorep.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brorep.Persistence
{
    public class BrorepDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Season> Seasons { get; set; }

        public BrorepDbContext(DbContextOptions<BrorepDbContext> options)
            : base(options)
        {
        }
    }
}
