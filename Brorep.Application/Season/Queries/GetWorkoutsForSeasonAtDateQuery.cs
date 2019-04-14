using AutoMapper;
using Brorep.Application.Exceptions;
using Brorep.Application.Season.Models;
using Brorep.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Season.Queries
{
    public class GetWorkoutsForSeasonAtDateQuery : IRequest<SeasonWorkoutsDto>
    {
        public GetWorkoutsForSeasonAtDateQuery(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }

    public class GetWorkoutsForCurrentSeasonQueryHandler : IRequestHandler<GetWorkoutsForSeasonAtDateQuery, SeasonWorkoutsDto>
    {
        private readonly BrorepDbContext _context;
        private readonly IMapper _mapper;

        public GetWorkoutsForCurrentSeasonQueryHandler(BrorepDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<SeasonWorkoutsDto> Handle(GetWorkoutsForSeasonAtDateQuery request, CancellationToken cancellationToken)
        {
            var season = await _context.Seasons
                .Where(x => x.Start < request.Date && x.End > request.Date)
                .Include(x => x.Workouts)
                .SingleOrDefaultAsync(cancellationToken);

            if(season == null) {
                throw new NotFoundException(nameof(Domain.Entities.Season), request.Date);
            }

            return _mapper.Map<SeasonWorkoutsDto>(season);
        }
    }
}
