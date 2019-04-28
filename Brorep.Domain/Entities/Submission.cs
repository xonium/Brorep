using System;
using System.Collections.Generic;

namespace Brorep.Domain.Entities
{
    public class Submission
    {
        public Guid SubmissionId { get; set; }

        public bool IsActive { get; set; }

        public DateTime Submitted { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Workout Workout { get; set; }

        public virtual Video Video { get; set; }

        public virtual ICollection<Rep> Reps { get; set; }
    }
}
