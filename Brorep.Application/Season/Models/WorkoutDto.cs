
using Brorep.Application.Interfaces.Mapping;
using System;

namespace Brorep.Application.Season.Models
{
    public class WorkoutDto : IMapFrom<Domain.Entities.Workout>
    {
        public Guid WorkoutId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
    }
}
