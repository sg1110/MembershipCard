using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Verify
{
    public class WhenCardIsNotRegistered : TestBase
    {

        public WhenCardIsNotRegistered(WebApplicationFactory<Startup> factory) : base(factory)
        {

        }


        [Fact]
        public async Task Will_return_404()
        {
            var client = Factory.CreateClient();

            var response = await client.GetAsync("card/verify/randomID");
            
            Assert.Equal((HttpStatusCode) StatusCodes.Status404NotFound, response.StatusCode);
        }
    }
}