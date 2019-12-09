using System;
using System.Data.Common;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
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