using System;
using System.Collections.Generic;

namespace Brorep.Domain.Entities
{
    public class Season
    {
        public int SeasonId { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public virtual ICollection<Workout> Workouts { get; set; }
    }
}
