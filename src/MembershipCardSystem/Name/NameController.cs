using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn;
using MembershipCardSystem.LogIn.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MembershipCardSystem.Name
{
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IMembershipCardRepository _cardRepository;
        private CachingPin _cachingPin;
        private const string ErrorMessage = "One or more validation errors occurred";


        public NameController(IMembershipCardRepository cardRepository, CachingPin cachingPin)
        {
            _cardRepository = cardRepository;
            _cachingPin = cachingPin;
        }

        [HttpGet]
        [Route("membershipcard/name/{cardId}")]

        public async Task<IActionResult> Add(string cardId)
        {
            try
            {
                var result = await _cardRepository.GetName(cardId);

                return Ok(new NameResponse(result.Name));
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
    }
}