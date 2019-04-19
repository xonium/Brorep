using FluentValidation;

namespace Brorep.Application.Workout.Commands
{
    public class SubmitWorkoutCommandValidator : AbstractValidator<SubmitWorkoutCommand>
    {
        public SubmitWorkoutCommandValidator()
        {
            RuleFor(x => x.Reps).NotEmpty();
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.VideoUrl).NotEmpty();
            RuleFor(x => x.WorkoutId).NotEmpty();
        }
    }
}
