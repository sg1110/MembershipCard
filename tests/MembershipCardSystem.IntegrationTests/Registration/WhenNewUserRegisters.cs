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
            var requestModel = new CardDetails
            {
                EmployeeId = "IDSTUFF",
                FirstName = "Name",
                SecondName = "Surname",
                MobileNumber = "01234"
            };
  
            return _system.Scenario(_ => _.Post.Json(requestModel
               )
                .ToUrl("/membershipcard/register"));
            
            
        }
        
        [Fact (Skip = "Currently tested without alba")]
        public async Task Will_return_no_content_result()
        {
            var response = await When_post_request_made();
            
           // Assert.Equal(201, response.Context.Response.StatusCode);
            
            response.Context.Response.StatusCode.Should().Be(201);
        }
    }
}
