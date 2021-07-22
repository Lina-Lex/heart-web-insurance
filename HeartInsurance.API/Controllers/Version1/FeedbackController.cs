using Application.Common.Responses;
using Application.Handlers.Commands;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HeartInsurance.API.Controllers
{
    [Route("feedback")]
    [Produces(MediaTypeNames.Application.Json)]
    public class FeedbackController : BaseEntryController
    {

        /// <summary>
        /// The endpoint for user register a feedback.
        /// </summary>
        /// <param name="command">The request payload</param>
        /// <returns>Expected to return success if required payload entered.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ResponseModel), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Post([FromHeader]string token, [FromBody]RegisterFeedbackCommand command)
        {
            command.SetToken(token);
            return Ok(await Mediator.Send(command));
        }
    }
}
