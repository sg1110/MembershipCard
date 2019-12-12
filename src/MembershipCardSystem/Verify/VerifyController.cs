using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Verify.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.Verify
{
    [ApiController]
    public class VerifyController  : ControllerBase
    {
        private readonly IMembershipCardRepository _cardrepository;

        public VerifyController(IMembershipCardRepository cardRepository)
        {
            _cardrepository = cardRepository;
        }

        [HttpGet]
        [Route("membershipcard/verify/{cardId}")]
        public async Task<IActionResult> VerifyRegistrationStatus(string cardId)
        {
            try
            {
                var result = await _cardrepository.VerifyCardRegistration(cardId);

                if (result.CardId.Any()) return Ok(new CardRegistrationStatusResult(result.CardId, result.Pin));
                
                return new NotFoundObjectResult(NotFound());

            }
            
            
            catch (DbException e)
            { 
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
        }
    }
}