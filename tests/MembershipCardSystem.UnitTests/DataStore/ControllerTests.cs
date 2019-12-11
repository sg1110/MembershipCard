using System.Net;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore;
using MembershipCardSystem.Registration;
using MembershipCardSystem.Registration.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace MembershipCardSystem.UnitTests.DataStore
{
    public class ControllerTests
    {
        private readonly RegistrationController _controller;


        public ControllerTests()
        {
            var membershipcardrepository = new Mock<IMembershipCardRepository>();

            _controller = new RegistrationController(membershipcardrepository.Object);
        }

        [Fact(Skip = "Tested through other integration tests")]
        public async Task Registers_with_valid_request()
        {
            
            var requestModel = new CardDetails
            {
                EmployeeId = "ID1",
                FirstName = "Name",
                SecondName = "Surname",
                MobileNumber = "01234"
            };

            var response = await _controller.Add(requestModel) as ObjectResult;
            
            Assert.Equal((int)HttpStatusCode.NoContent, response.StatusCode);

        }
    }
}