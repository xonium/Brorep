
using System;

namespace Brorep.Application.Season.Models
{
    public class WorkoutDto
    {
        public Guid WorkoutId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
