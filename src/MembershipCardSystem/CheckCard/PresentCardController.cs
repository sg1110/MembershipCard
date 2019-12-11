using System;
using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.CheckCard.Model;
using MembershipCardSystem.DataStore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace MembershipCardSystem.CheckCard
{
    [ApiController]
    public class PresentCardController : ControllerBase
    {
        private IMembershipCardRepository _cardrepository;

        public PresentCardController(IMembershipCardRepository cardRepository)
        {
            _cardrepository = cardRepository;
        }

//        [HttpPost]
//        [Route("membershipcard/presentcard")]
//        public async Task<IActionResult> VerifyCard([FromBody] PresentCard presentCard)
//        {
//            try
//            {
//                await _cardrepository.VerifyCardDetails(
//                    presentCard.CardId,
//                    presentCard.Pin);
//            }
//            catch (DbException e)
//            {
//                Console.WriteLine(e);
//                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
//            }

            //  return OkObjectResult(result);
       // }
    }
}

