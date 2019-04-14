using System;

namespace Brorep.Domain.Entities
{
    public class Workout
    {
        public Guid WorkoutId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string VideoUrl { get; set; }

        public int Length { get; set; }
    }
}
