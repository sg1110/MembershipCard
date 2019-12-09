using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.StatusTests
{
    public class StatusTestWithoutAlba: TestBase
    {
        public StatusTestWithoutAlba(WebApplicationFactory<Startup> factory) : base(factory)
        {
        }
        
        [Fact]
        public async void Will_return_application_status()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("/membershipcard/v1/_status");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
        
        [Fact]
        public async void Will_return_application_versiom()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("/membershipcard/v1/_status");
            
            Assert.Equal("1.0.0", response.Version.ToString());
        }
    }
}

