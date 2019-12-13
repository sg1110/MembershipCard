using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.LogIn.Model;
using MembershipCardSystem.Name.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.Name
{
    [ApiController]
    public class NameController : ControllerBase
    {
        private readonly IMembershipCardRepository _cardRepository;
        private const string ErrorMessage = "One or more validation errors occurred";


        public NameController(IMembershipCardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        [HttpGet]
        [Route("membershipcard/name/{cardId}")]
        [SwaggerOperation("Retrieves name associated with provided card id")]
        [SwaggerResponse(200, "Name has been retrieved successfully")]
        [SwaggerResponse(404, "Card id provided does not exisr")]
        [SwaggerResponse(500, Description = "Unexpected database failure")]
        [SwaggerRequestExample(typeof(NameResponse), typeof(NameResponseModelExample))]


        public async Task<IActionResult> Add(string cardId)
        {
            try
            {
                var result = await _cardRepository.GetName(cardId);

                if (string.IsNullOrEmpty(result.Name)) return StatusCode(StatusCodes.Status404NotFound);

                return Ok(new NameResponse(result.Name));
            }

            catch (DbException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}