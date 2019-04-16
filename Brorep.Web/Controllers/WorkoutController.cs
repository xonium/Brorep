﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Brorep.Application.Season.Models;
using Brorep.Application.Workout.Queries;
using Brorep.Common.Extensions;

namespace Brorep.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutController : BaseController
    {
        [HttpGet]
        [Route("{seasonName}/{workoutName}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(WorkoutDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetWorkout(string seasonName, string workoutName)
        {
            var query = new GetWorkoutByNameForSeasonQuery()
            {
                SeasonName = seasonName.ReplaceInputUrlMinusSignWithSpace(),
                WorkoutName = workoutName.ReplaceInputUrlMinusSignWithSpace()
            };

            var workouts = await Mediator.Send(query);
            return Ok(workouts);
        }
    }
}