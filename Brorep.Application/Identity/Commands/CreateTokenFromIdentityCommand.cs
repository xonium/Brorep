using System;
using System.Collections.Generic;
using System.Text;

namespace Brorep.Application.Identity.Commands
{
    public class CreateTokenFromIdentityCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
