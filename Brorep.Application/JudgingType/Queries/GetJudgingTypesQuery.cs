using AutoMapper;
using Brorep.Application.JudgingType.Models;
using Brorep.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.JudgingType.Queries
{
    public class GetJudgingTypesQuery : IRequest<JudgingTypesDto>
    {
    }

    public class GetJudgingTypesQueryHandler : IRequestHandler<GetJudgingTypesQuery, JudgingTypesDto>
    {
        private readonly BrorepDbContext _context;
        private readonly IMapper _mapper;

        public GetJudgingTypesQueryHandler(BrorepDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JudgingTypesDto> Handle(GetJudgingTypesQuery request, CancellationToken cancellationToken)
        {
            var judgingTypes = await _context.JudgingTypes.ToListAsync(cancellationToken);
            var judgingTypesReturn = new JudgingTypesDto()
            {
                JudgingTypes = judgingTypes?.Select(x => _mapper.Map<JudgingTypeDto>(x))?.ToList() ?? new List<JudgingTypeDto>()
            };

            return judgingTypesReturn;
        }
    }
}
