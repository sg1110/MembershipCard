using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alba;
using FluentAssertions;
using MembershipCardSystem.Registration.Model;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenNewUserRegisters :IClassFixture<WebAppFixture>
    {
        private readonly SystemUnderTest _system;

        public WhenNewUserRegisters(WebAppFixture app)
        {
            _system = app.SystemUnderTest;
        }

        private Task<IScenarioResult> When_post_request_made()
        {

//            var request =
//                "{\"EmployeeId\": \"TestId12345\"," +
//                "\"FirstName\": \"TestName\"," +
//                "\"SecondName\": \"TestName\"," +
//                "\"MobileNumber\": \"0123456789\"}";
//            
//            var nonsense = "string";
//            
//            var stringrequest = new StringContent(
//                "{\"EmployeeId\": \"TestId1\",\"FirstName\": \"TestName\",\"SecondName\": \"TestName\",\"MobileNumber\": \"0123456789\"}",
//                Encoding.UTF8, "application/json");
            
            var requestModel = new CardDetails
            {
                EmployeeId = "ID416",
                FirstName = "Name",
                SecondName = "Surname",
                MobileNumber = "01234"
            };
  
            return _system.Scenario(_ => _.Post.Json(requestModel
               )
                .ToUrl("/membershipcard/register"));
            
            
        }
        
        [Fact]
        public async Task Will_return_no_content_result()
        {
            var response = await When_post_request_made();
            
            response.Context.Response.StatusCode.Should().Be(200);
        }
    }
}