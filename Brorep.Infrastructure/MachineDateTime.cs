using Brorep.Common;
using System;

namespace Brorep.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.UtcNow;
    }
}
