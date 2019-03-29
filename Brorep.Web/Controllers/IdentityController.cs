using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Brorep.Application.Identity.Commands;
using Brorep.WebUI.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace Brorep.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private IUserService _userService;
        public IdentityController(IUserService userService)
        {
            _userService = userService;
        }

        // POST api/identity/register
        [HttpPost]
        [Route("register")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Register([FromBody]CreateIdentityCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        // POST api/identity/signin
        [HttpPost]
        [Route("signin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public IActionResult SignIn([FromBody]CreateTokenFromIdentityCommand command)
        {
            return Ok(_userService.Authenticate(command.Username, command.Password));
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public IActionResult GetAll()
        {
            return Ok("DET GICK BRA");
        }

    }
}