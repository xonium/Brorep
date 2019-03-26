using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Brorep.Application.Identity.Commands;

namespace Brorep.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
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

    }
}