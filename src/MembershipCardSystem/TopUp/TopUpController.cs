//using System.Threading.Tasks;
//using MembershipCardSystem.DataStore;
//using MembershipCardSystem.TopUp.Model;
//using Microsoft.AspNetCore.Mvc;
//
//namespace MembershipCardSystem.TopUp
//{
//    [ApiController]
//    public class TopUpController : ControllerBase
//    {
//        private IMembershipCardRepository _cardRepository;
//        private CachingPin _cachingPin;
//
//        public TopUpController(IMembershipCardRepository cardRepository, CachingPin cachingPin)
//        {
//            _cardRepository = cardRepository;
//            _cachingPin = cachingPin;
//        }
//
//        [HttpPost]
//        [Route("membershipcard/topup")]
//
//        public async Task<IActionResult> Add([FromBody] TopUpRequest topUpRequest)
//        {
//            //is this still needed?
//            if (topUpRequest == null)
//            {
//                return BadRequest("Request body is empty");
//            }
//
//            if (IsPinSent(topUpRequest))
//            {
//               var pinCorrect = await _cardRepository.VerifyPin(topUpRequest.CardId, topUpRequest.Pin);
//                //if pins  is correct = true increase balance and cache pin
//                
//           //    var result = await TopUpCard(topUpRequest.CardId, topUpRequest.Pin, topUpRequest.Amount);
////                 _cachingPin.CachePin(topUpRequest.Pin,  topUpRequest.CardId);
////                return Ok("new balance blablabla");
//
//                //if false return 401
//            }
//
//
//            if (!IsPinSent(topUpRequest) && !CardPinCached(topUpRequest.CardId))
//            {
//                return BadRequest("Your session timed out, please provide your pin");
//            }
//
//
//            if (!IsPinSent(topUpRequest) && CardPinCached(topUpRequest.CardId))
//            {
//                //skip verify and just top up
//            }
//            
//            //everything else.....catch db errors and check for validation like in reg.
//           
//        }
//
////        public async Task TopUpCard(string cardId, string cardPin, int amount )
////        {
////            var result = await _cardRepository.updateBalance(cardId, cardId, amount);
////            return (new TopUpResponse("", ""));
////        }
//
//        public bool CardPinCached(string cardId)
//        {
//            if (_cachingPin.IsTokenCached(cardId))
//            {
//                return true;
//            };
//
//            return false;
//        }
//
//        public bool IsPinSent(TopUpRequest request)
//        {
//            return !string.IsNullOrEmpty(request.Pin);
//        }
//    }
//}