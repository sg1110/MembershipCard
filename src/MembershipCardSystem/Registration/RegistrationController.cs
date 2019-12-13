using System;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Registration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MembershipCardSystem.Registration
{
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IMembershipCardRepository _cardRepository;
        private const string ErrorMessage = "Employee ID is already registered";


        public RegistrationController(IMembershipCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpPost]
        [Route("membershipcard/register")]
 //       [SwaggerResponse(201, Description = "New card details have been registered")]

        public async Task<IActionResult> Add([FromBody] CardDetails cardDetails)
        {
            //is this still needed?
            if (cardDetails == null)
            {
                return BadRequest("Request body is empty");
                
            }
            
            try
            {
                await _cardRepository.SaveRegistrationDetails(cardDetails.EmployeeId,
                    cardDetails.FirstName,
                    cardDetails.SecondName,
                    cardDetails.MobileNumber);
                
                return Created("", null);
            }
            catch (DbException e)
            {
                if (e.Message.Contains("Cannot insert duplicate key in object 'dbo.Card").Equals(true))
                {
                    var jsonErrorMessage = new JsonResult(ErrorMessage);
                    return StatusCode(StatusCodes.Status500InternalServerError, jsonErrorMessage);

                }
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            
     

        }
        
        
        [HttpGet]
        [Route("membershipcard/all")]
        public async Task<ActionResult> Get()
        {

            try
            {
                var result = await _cardRepository.GetAll();
                return Ok(result);
            }
            catch (DbException e)
            {
                Console.WriteLine(e);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

    }
}