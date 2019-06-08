using Brorep.Application.JudgingSession.Models;
using Brorep.Application.JudgingSession.Queries;
using Brorep.Application.JudgingType.Models;
using Brorep.Application.JudgingType.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Brorep.Ui.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JudgingController : BaseController
    {
        [HttpGet]
        [Route("types")]
        [ProducesResponseType(typeof(JudgingTypesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetJudgingTypes()
        {
            var query = new GetJudgingTypesQuery();
            var judgingTypes = await Mediator.Send(query);
            return Ok(judgingTypes);
        }

        [HttpGet]
        [Route("session")]
        [ProducesResponseType(typeof(JudgingSessionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetJudgingSession(Guid type)
        {
            var query = new GetJudgingSessionQuery(type);
            var judgingSession = await Mediator.Send(query);
            return Ok(judgingSession);
        }
    }
}
