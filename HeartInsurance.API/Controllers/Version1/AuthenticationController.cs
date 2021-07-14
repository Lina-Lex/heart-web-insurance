using Application.Common.Responses;
using Application.Handlers.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HeartInsurance.API.Controllers.Version1
{
    [Route("auths")]
    [Produces(MediaTypeNames.Application.Json)]
    public class AuthenticationController : BaseEntryController
    {

        /// <summary>
        /// The endpoint for user signing up.
        /// </summary>
        /// <param name="command">The request payload</param>
        /// <returns>Expected to return success if required payload entered.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.InternalServerError)]
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpCommand command)
            => Ok(await Mediator.Send(command));

        /// <summary>
        /// User login endpoint
        /// </summary>
        /// <param name="command">The request payload</param>
        /// <returns>Expected to return success if required payload entered.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.InternalServerError)]
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] UserSignInCommand command)
            => Ok(await Mediator.Send(command));

        /// <summary>
        /// The endpoint verify generated passcode
        /// </summary>
        /// <param name="command">The request for confirmation</param>
        /// <returns>Expected to confirm the user's email.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [HttpPost("validate-passcode")]
        public async Task<IActionResult> ValidatePassCode([FromBody] VerifyPassCodeThenLoginCommand command)
            => Ok(await Mediator.Send(command));

        /// <summary>
        /// The endpoint email confirmation
        /// </summary>
        /// <param name="token">Unique generated token for email confirmation</param>
        /// <param name="command">The request for confirmation</param>
        /// <returns>Expected to confirm the user's email.</returns>
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [HttpPost("email/confirm-email-address")]
        public async Task<IActionResult> ConfirmEmail([FromHeader] string token, [FromBody] EmailConfirmationCommand command)
        {
            command.Token = token;
            return Ok(await Mediator.Send(command));
        }
    }
}
