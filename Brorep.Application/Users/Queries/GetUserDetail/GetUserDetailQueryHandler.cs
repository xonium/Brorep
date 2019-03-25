using Brorep.Application.Exceptions;
using Brorep.Domain;
using Brorep.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Users.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, UserDetailModel>
    {
        private readonly BrorepDbContext _context;

        public GetUserDetailQueryHandler(BrorepDbContext context)
        {
            _context = context;
        }

        public async Task<UserDetailModel> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Users.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(User), request.Id);
            }

            return UserDetailModel.Create(entity);
        }
    }
}
