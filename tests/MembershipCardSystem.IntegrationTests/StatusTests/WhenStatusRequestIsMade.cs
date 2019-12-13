using System.Threading.Tasks;
using Alba;
using FluentAssertions;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.StatusTests
{
    public class WhenStatusRequestIsMade : IClassFixture<WebAppFixture>
    {
        private readonly SystemUnderTest _system;

        public WhenStatusRequestIsMade(WebAppFixture app)
        {
            _system = app.SystemUnderTest;
        }

        private Task<IScenarioResult> When_request_made()
        {
            return _system.Scenario(_ => _.Get.Url("/card/v1/_status"));
        }


        [Fact]
        public async Task Will_return_ok_response()
        {
            var response = await When_request_made();

            response.Context.Response.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Will_return_application_status()
        {
            var response = await When_request_made();

            Assert.Equal("OK", response.ResponseBody.ReadAsJson<dynamic>().status.ToString());
        }

        [Fact]
        public async Task Will_return_application_version()
        {
            var response = await When_request_made();

            Assert.Equal("1.0.0", response.ResponseBody.ReadAsJson<dynamic>().version.ToString());
        }
    }

}