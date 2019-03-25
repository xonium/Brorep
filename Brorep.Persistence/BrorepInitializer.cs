using Brorep.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Brorep.Persistence
{
    public class BrorepInitializer
    {
        
        private readonly Dictionary<int, User> Users = new Dictionary<int, User>();

        public static void Initialize(BrorepDbContext context)
        {
            var initializer = new BrorepInitializer();
            initializer.SeedEverything(context);
        }

        public void SeedEverything(BrorepDbContext context)
        {
            context.Database.EnsureCreated();

            

            //if (context.Users.Any())
            //{
            //    return; // Db has been seeded
            //}

            //SeedUsers(context);
        }

        private void SeedUsers(BrorepDbContext context)
        {
            /*Users.Add(1, new User { Email = "mail@temp.se", Firstname = "Charlotte", Lastname = "Purchasingson" });

            foreach (var user in Users.Values)
            {
                context.Users.Add(user);
            }

            context.SaveChanges();*/
        }
    }
}
