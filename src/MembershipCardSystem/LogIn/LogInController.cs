using System;
using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.LogIn
{
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IMembershipCardRepository _cardrepository;
        private CachingPin _cachingPin;
        private const string ErrorMessage = "Missing required field";

        public LogInController(IMembershipCardRepository cardRepository,  CachingPin cachingPin)
        {
            _cardrepository = cardRepository;
            _cachingPin = cachingPin;
        }

        [HttpPost]
        [Route("membershipcard/login")]
        [SwaggerOperation("Log in users card ")]
        [SwaggerResponse(204, "The card has been logged in")]
        [SwaggerResponse(400, ErrorMessage)]
        [SwaggerRequestExample(typeof(LogInRequest), typeof(LogInRequestModelExample))]

        public async Task<IActionResult> LogIn([FromBody] LogInRequest logInRequest)
        {
            try
            {
                var currentPin = await _cardrepository.GetPin(logInRequest.CardId);

                bool IsPinCorrect(string storedPin, string requestPin)
                {
                    return storedPin == requestPin;
                }

                if (IsPinCorrect(currentPin, logInRequest.CardPin))
                {
                    _cachingPin.CachePin(logInRequest.CardPin, logInRequest.CardId);
                };

                if (!IsPinCorrect(currentPin, logInRequest.CardPin))
                {
                    return Unauthorized();
                }

                return NoContent();
            }

            catch (DbException e)
            { 
                if (e.Message.Contains("One or more validation errors occurred").Equals(true))
                {
                    var jsonErrorMessage = new JsonResult(e.Message);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonErrorMessage);

                }
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
        }
        
    }
}
