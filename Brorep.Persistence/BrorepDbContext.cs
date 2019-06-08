using Brorep.Domain.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Brorep.Persistence
{
    public class BrorepDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<Submission> Submissions { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Rep> Reps { get; set; }
        public DbSet<Score> Scores { get; set; }
        public DbSet<JudgingType> JudgingTypes { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public BrorepDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
    }
}
