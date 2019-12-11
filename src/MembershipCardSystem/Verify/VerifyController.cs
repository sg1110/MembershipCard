using System;
using System.Data.Common;
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
        private IMembershipCardRepository _cardrepository;

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
                return Ok(new CardRegistrationStatusResult(result.CardId));
            }
            catch (DbException e)
            { 
                Console.WriteLine(e);
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");

            }
        }
    }
}