using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brorep.Persistence
{
    public class BrorepDbContext : IdentityDbContext
    {
        public BrorepDbContext(DbContextOptions<BrorepDbContext> options)
            : base(options)
        {
        }
    }
}
