using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Alba;
using FluentAssertions;
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

            var request =
                "{\"EmployeeId\": \"TestId1\",\"FirstName\": \"TestName\",\"SecondName\": \"TestName\",\"MobileNumber\": \"0123456789\"}";
            var nonsense = "string";
            
            var stringrequest = new StringContent(
                "{\"EmployeeId\": \"TestId1\",\"FirstName\": \"TestName\",\"SecondName\": \"TestName\",\"MobileNumber\": \"0123456789\"}",
                Encoding.UTF8, "application/json");

  
            return _system.Scenario(_ => _.Post.Json(request
               )
                .ToUrl("/membershipcard/register"));
            
            
        }
        
        [Fact]
        public async Task Will_return_ok_result()
        {
            var response = await When_post_request_made();
            response.Context.Response.StatusCode.Should().Be(200);
        }
    }
}