using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Registration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.Registration
{
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IMembershipCardRepository _cardRepository;
        private const string ErrorMessage = "Employee ID or CardID is already registered";


        public RegistrationController(IMembershipCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpPost]
        [Route("membershipcard/register")]
        [SwaggerOperation("Registers new card into the membership card system")]
        [SwaggerResponse(200, Description = "New card details have been registered")]
        [SwaggerResponse(400, Description = "Required information is missing in the request")]
        [SwaggerResponse(500, Description = "Unexpected database failure")]
        [SwaggerRequestExample(typeof(RegistrationRequest), typeof(RegistrationRequestModelExample))]
        
        public async Task<IActionResult> Add([FromBody] RegistrationRequest registrationRequest)
        {
            try
            {
                await _cardRepository.SaveRegistrationDetails(registrationRequest.EmployeeId,
                    registrationRequest.FirstName,
                    registrationRequest.SecondName,
                    registrationRequest.MobileNumber,
                    registrationRequest.CardId);
                
                return Ok ("Card details registered");
            }
            catch (DbException e)
            {
                if (e.Message.Contains("Cannot insert duplicate key in object 'dbo.Card").Equals(true))
                {
                    var jsonErrorMessage = new JsonResult(ErrorMessage);
                    return StatusCode(StatusCodes.Status400BadRequest, jsonErrorMessage);

                }
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}