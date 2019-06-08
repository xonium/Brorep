using System;

namespace Brorep.Domain.Entities
{
    public class Grade
    {
        public Guid GradeId { get; set; }
        public virtual ApplicationUser Judge { get; set; }
        public virtual Rep Rep { get; set; }
        public int Score { get; set; }
        public DateTime Reported { get; set; }
    }
}
