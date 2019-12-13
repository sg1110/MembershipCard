using System;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Verify.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
        [SwaggerOperation("Returns cardId if it has been registered, informs if pin is present")]
        [SwaggerResponse(404, "Card has not been registered")]
        [SwaggerResponse(200, "Card has been registered")]
        [SwaggerResponse(500, Description = "Unexpected database failure")]
        public async Task<IActionResult> VerifyRegistrationStatus(string cardId)
        {
            try
            {
                var result = await _cardrepository.VerifyCardRegistration(cardId);

                if (result.CardId.Any()) return Ok(new CardRegistrationVerificationResult(result.CardId, result.Pin));
                
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