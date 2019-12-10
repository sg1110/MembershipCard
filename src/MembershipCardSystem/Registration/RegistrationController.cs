using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Registration.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.Registration
{
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private IMembershipCardRepository _cardRepository;

        public RegistrationController(IMembershipCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpPost]
        [Route("membershipcard/register")]
        public async Task<IActionResult> Add([FromBody] CardDetails cardDetails)
        {
            if (cardDetails == null)
            {
                return BadRequest("Request body is empty");
                
            }
//            if (!ModelState.IsValid)
//            {
//                var errors = ModelState.Select(x => x.Value.Errors)
//                    .Where(y => y.Count > 0)
//                    .ToList();
//                
//                return BadRequest(errors);
//            }

            try
           {
               await _cardRepository.SaveRegistrationDetails(cardDetails.EmployeeId,
                   cardDetails.FirstName,
                   cardDetails.SecondName,
                   cardDetails.MobileNumber);
           }
           catch (DbException e)
           {
               Console.WriteLine(e);
               return this.StatusCode(StatusCodes.Status500InternalServerError, e.Message);
               //return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
           }

            
            return NoContent();

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