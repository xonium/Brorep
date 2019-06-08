using Brorep.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Brorep.Persistence
{
    public class BrorepInitializer
    {
        private static UserManager<ApplicationUser> _userManager;

        private readonly List<ApplicationUser> Users = new List<ApplicationUser>();
        private readonly List<Season> Seasons = new List<Season>();
        private readonly List<JudgingType> JudgingTypes = new List<JudgingType>();

        public static void Initialize(BrorepDbContext context, UserManager<ApplicationUser> userManager)
        {
            var initializer = new BrorepInitializer();
            _userManager = userManager;

            initializer.SeedEverything(context);            
        }

        public void SeedEverything(BrorepDbContext context)
        {
            if (context.Users.Any())
            {
                return; // Db has been seeded
            }

            SeedUsers(context);
            SeedSeasons(context);
            SeedJudgingTypes(context);
        }

        private void SeedSeasons(BrorepDbContext context)
        {
            Seasons.Add(new Season {Name="Season zero", Start = DateTime.UtcNow.AddDays(-90), End = DateTime.UtcNow.AddDays(-1),
                Workouts = new List<Workout> { new Workout { Name = "Workout 1" } }
            });
            Seasons.Add(new Season { Name = "Season one", Start = DateTime.UtcNow, End = DateTime.UtcNow.AddDays(90),
                Workouts = new List<Workout> { new Workout { Name = "Workout 2" } }
            });
            Seasons.Add(new Season { Name = "Season two", Start = DateTime.UtcNow.AddDays(90), End = DateTime.UtcNow.AddDays(180),
                Workouts = new List<Workout> { new Workout { Name = "Workout 3" } }
            });

            context.Seasons.AddRange(Seasons);
            context.SaveChanges();
        }

        private void SeedUsers(BrorepDbContext context)
        {
            Users.Add(new ApplicationUser { UserName = "HerrNilsson", PhoneNumber = "123" });
            Users.Add(new ApplicationUser { UserName = "Fluff", PhoneNumber = "123" });
            Users.Add(new ApplicationUser { UserName = "BoByggare", PhoneNumber = "123"  });

            foreach (var user in Users)
            {
                var result = _userManager.CreateAsync(user, user.UserName + user.PhoneNumber + "!").Result;
                if(result.Succeeded)
                {

                }
            }

            context.SaveChanges();
        }

        private void SeedJudgingTypes(BrorepDbContext context)
        {
            JudgingTypes.Add(new JudgingType { Name = "10 Reps", Description = "Set of 10 repititions to judge" });

            context.AddRange(JudgingTypes);
            context.SaveChanges();
        }
    }
}
