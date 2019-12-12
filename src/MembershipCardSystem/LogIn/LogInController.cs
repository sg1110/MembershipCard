using System;
using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.LogIn
{
    [ApiController]
    public class LogInController : ControllerBase
    {
        private readonly IMembershipCardRepository _cardrepository;
        private CachingPin _cachingPin;
        private const string ErrorMessage = "Employee ID is already registered";

        public LogInController(IMembershipCardRepository cardRepository,  CachingPin cachingPin)
        {
            _cardrepository = cardRepository;
            _cachingPin = cachingPin;
        }

        [HttpPost]
        [Route("membershipcard/login")]
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
                if (e.Message.Contains("Cannot insert duplicate key in object 'dbo.Card").Equals(true))
                {
                    var jsonErrorMessage = new JsonResult(ErrorMessage);
                    return StatusCode(StatusCodes.Status500InternalServerError, jsonErrorMessage);

                }
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
        }
        
    }
}