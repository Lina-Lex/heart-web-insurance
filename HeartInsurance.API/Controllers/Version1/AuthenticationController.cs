using Application.Common.Responses;
using Application.Handlers.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HeartInsurance.API.Controllers.Version1
{
    [Route("Authentications")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthenticationController : BaseEntryController
    {

        /// <summary>
        /// The endpoint for user signing up.
        /// </summary>
        /// <param name="command">The request payload</param>
        /// <returns>Expected to return success if required payload entered.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.InternalServerError)]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpCommand command)
            => Ok(await Mediator.Send(command));
    }
}
