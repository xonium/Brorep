using System;

namespace Brorep.Domain.Entities
{
    public class Score
    {
        public Guid ScoreId { get; set; }

        public double Points { get; set; }

        public DateTime Date { get; set; }

        public virtual Rep Rep { get; set; }

        public virtual ApplicationUser ByUser { get; set; }
    }
}
