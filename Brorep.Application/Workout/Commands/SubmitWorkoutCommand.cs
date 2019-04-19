using Brorep.Application.Exceptions;
using Brorep.Application.Workout.Models;
using Brorep.Common;
using Brorep.Domain.Entities;
using Brorep.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Brorep.Application.Workout.Commands
{
    public class SubmitWorkoutCommand : IRequest
    {
        public string Username { get; set; }
        public Guid WorkoutId { get; set; }

        public List<RepDto> Reps { get; set; }

        public string VideoUrl { get; set; }
    }

    public class SubmitWorkoutCommandHandler : IRequestHandler<SubmitWorkoutCommand, Unit>
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly BrorepDbContext _context;
        private readonly IDateTime _dateTime;

        public SubmitWorkoutCommandHandler(
            UserManager<ApplicationUser> userManager,
            BrorepDbContext brorepDbContext,
            IDateTime dateTime)
        {
            _userManager = userManager;
            _context = brorepDbContext;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(SubmitWorkoutCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            var workout = await _context.Workouts
                .Where(x => x.WorkoutId == request.WorkoutId)
                .SingleOrDefaultAsync();

            if(user == null)
            {
                throw new NotFoundException(nameof(ApplicationUser), request.Username);
            };

            if (workout == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.Workout), request.WorkoutId);
            };

            var video = new Video()
            {
                Url = request.VideoUrl
            };

            var reps = new List<Rep>();
            reps.AddRange(request.Reps
                .Select(x => 
                    new Rep()
                        {
                            Start = x.StartTime,
                            Stop = x.StopTime
                        }
                ));

            _context.Videos.Add(video);

            try { 
                await _context.SaveChangesAsync(cancellationToken);

                var previousSubmissions = await _context.Submissions
                    .Where(x => x.User.Id == user.Id && x.Workout.WorkoutId == workout.WorkoutId)
                    .ToListAsync();

                if (previousSubmissions.Any())
                {
                    previousSubmissions.ForEach((s) => { s.IsActive = false; });                    
                    await _context.SaveChangesAsync(cancellationToken);
                }

                var submission = new Submission()
                {
                    Submitted = _dateTime.Now,
                    User = user,
                    Video = video,
                    IsActive = true,
                    Workout = workout,
                    Reps = reps
                };

                await _context.Submissions.AddAsync(submission);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch(DbUpdateException dbUpdate)
            {

            }

            return Unit.Value;
        }
    }
}
