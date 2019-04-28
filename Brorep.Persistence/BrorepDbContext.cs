﻿using Brorep.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Brorep.Persistence
{
    public class BrorepDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Rep> Reps { get; set; }
        public DbSet<Score> Scores { get; set; }

        public BrorepDbContext(DbContextOptions<BrorepDbContext> options)
            : base(options)
        {
        }
    }
}
