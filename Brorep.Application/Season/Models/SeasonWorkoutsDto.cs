using System;
using System.Collections.Generic;

namespace Brorep.Application.Season.Models
{
    public class SeasonWorkoutsDto
    {
        public int SeasonId { get; set; }

        public string Name { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public ICollection<WorkoutDto> Workouts { get; set; }
    }
}
