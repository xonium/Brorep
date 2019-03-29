using System;

namespace Brorep.Application.Exceptions
{
    public class AuthenticationException : Exception
    {
        public AuthenticationException()
            : base($"Wrong username or password")
        {
        }
    }
}
