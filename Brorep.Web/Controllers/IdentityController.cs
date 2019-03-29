using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Brorep.Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;
using Brorep.Application.Identity.Models;
using Brorep.Application.Identity.Queries;

namespace Brorep.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody]CreateIdentityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPost]
        [Route("createtoken")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(TokenDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateToken([FromBody]CreateTokenFromIdentityCommand command)
        {
            var token = await Mediator.Send(command);
            return Ok(token);
        }

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUser()
        {
            var query = new GetCurrentIdentityQuery(User);
            var user = await Mediator.Send(query);
            return Ok(user);
        }
    }
}