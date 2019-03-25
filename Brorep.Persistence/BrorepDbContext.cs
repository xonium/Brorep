using Brorep.Domain;
using Microsoft.EntityFrameworkCore;
using System;

namespace Brorep.Persistence
{
    public class BrorepDbContext : DbContext
    {
        public BrorepDbContext(DbContextOptions<BrorepDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}
