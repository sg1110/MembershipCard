using System;
using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn;
using MembershipCardSystem.TopUp.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.TopUp
{
    [ApiController]
    public class TopUpController : ControllerBase
    {
        private readonly IMembershipCardRepository _cardRepository;
        private CachingPin _cachingPin;
        private const string ErrorMessage = "Employee ID is already registered";


        public TopUpController(IMembershipCardRepository cardRepository, CachingPin cachingPin)
        {
            _cardRepository = cardRepository;
            _cachingPin = cachingPin;
        }

        [HttpPut]
        [Route("membershipcard/topup/{cardId}")]

        public async Task<IActionResult> Add(string cardId,[FromBody] TopUpRequest topUpRequest)
        {
            try
            {
             //  var result = await _cardRepository.UpdateBalance(cardId, topUpRequest.TopUpAmount);
                
                return Ok(new TopUpResponse(1, 1));
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