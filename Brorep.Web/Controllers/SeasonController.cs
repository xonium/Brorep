using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Brorep.Application.Season.Queries;
using Brorep.Common;
using Brorep.Application.Season.Models;

namespace Brorep.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeasonController : BaseController
    {
        private readonly IDateTime _dateTime;

        public SeasonController(IDateTime dateTime)
        {
            _dateTime = dateTime;
        }

        [HttpGet]
        [Route("current")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SeasonWorkoutsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetWorkoutsForSeason()
        {
            var query = new GetWorkoutsForSeasonAtDateQuery(_dateTime.Now);
            var workouts = await Mediator.Send(query);
            return Ok(workouts);
        }
    }
}