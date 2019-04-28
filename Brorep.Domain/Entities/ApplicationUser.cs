using Microsoft.AspNetCore.Identity;

namespace Brorep.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Instagram_Username { get; set; }

        public int Coins { get; set; }
    }
}
