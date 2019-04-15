using Brorep.Application.Workout.Queries;
using FluentValidation;

namespace Brorep.Application.Identity.Commands
{
    public class GetWorkoutByNameForSeasonQueryValidator : AbstractValidator<GetWorkoutByNameForSeasonQuery>
    {
        public GetWorkoutByNameForSeasonQueryValidator()
        {
            RuleFor(x => x.SeasonName).NotEmpty();
            RuleFor(x => x.WorkoutName).NotEmpty();
        }
    }
}
