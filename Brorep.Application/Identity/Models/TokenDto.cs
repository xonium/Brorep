using System;

namespace Brorep.Application.Identity.Models
{
    public class TokenDto
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }
    }
}
