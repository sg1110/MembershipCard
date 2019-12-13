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
        private const string ErrorMessage = "One or more validation errors occurred";


        public TopUpController(IMembershipCardRepository cardRepository, CachingPin cachingPin)
        {
            _cardRepository = cardRepository;
            _cachingPin = cachingPin;
        }

        [HttpPut]
        [Route("membershipcard/topup/{cardId}")]

        public async Task<IActionResult> Add(string cardId,[FromBody] TopUpRequest topUpRequest)
        {
            if (!_cachingPin.IsPinCached(cardId)) return Unauthorized();
            {
                try
                {
                    //   var result = await _cardRepository.UpdateBalance(cardId, topUpRequest.TopUpAmount);

                    return Ok(new TopUpResponse(1, 1));
                }

                catch (DbException e)
                {
                    if (e.Message.Contains(ErrorMessage).Equals(true))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, e);

                    }

                    return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
                }
            }

            return Unauthorized();
        }
    }
}