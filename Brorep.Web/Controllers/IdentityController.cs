using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Brorep.Application.Identity.Commands;
using Microsoft.AspNetCore.Authorization;

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
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]CreateIdentityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPost]
        [Route("createtoken")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> CreateToken([FromBody]CreateTokenFromIdentityCommand command)
        {
            var token = await Mediator.Send(command);
            return Ok(token);
        }
    }
}