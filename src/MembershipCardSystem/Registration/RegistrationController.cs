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
//            if (cardDetails == null)
//            {
//                return BadRequest("Request body is empty");
//                
//            }
//            if (!ModelState.IsValid)
//            {
//                var errors = ModelState.Select(x => x.Value.Errors)
//                    .Where(y => y.Count > 0)
//                    .ToList();
//                
//                return BadRequest(errors);
//            }
            var cardId = RandomString(16);
            
            await _cardRepository.SaveRegistrationDetails(cardDetails.EmployeeId,
                cardDetails.FirstName,
                cardDetails.SecondName,
                cardDetails.MobileNumber,cardId);
            
            return NoContent();

        }
        
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
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