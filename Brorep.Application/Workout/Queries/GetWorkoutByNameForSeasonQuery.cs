using AutoMapper;
using Brorep.Application.Exceptions;
using Brorep.Application.Workout.Models;
using Brorep.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace Brorep.Application.Workout.Queries
{
    public class GetWorkoutByNameForSeasonQuery : IRequest<WorkoutDto>
    {
        public string WorkoutName { get; set; }

        public string SeasonName { get; set; }
    }

    public class GetWorkoutByNameForSeasonQueryHandler : IRequestHandler<GetWorkoutByNameForSeasonQuery, WorkoutDto>
    {
        private readonly BrorepDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkoutByNameForSeasonQueryHandler(BrorepDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<WorkoutDto> Handle(GetWorkoutByNameForSeasonQuery request, CancellationToken cancellationToken)
        {
            var workout = await _context.Seasons
                .Where(s => s.Name == request.SeasonName && s.Workouts.Any(w => w.Name == request.WorkoutName))
                .Include(x => x.Workouts)
                .Select(y => y.Workouts.FirstOrDefault())
                .SingleOrDefaultAsync(cancellationToken);

            if (workout == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Workout), request.WorkoutName);
            }

            return _mapper.Map<WorkoutDto>(workout);
        }
    }
}
