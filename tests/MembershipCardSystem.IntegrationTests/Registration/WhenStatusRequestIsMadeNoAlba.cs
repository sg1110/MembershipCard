using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenStatusRequestIsMadeNoAlba : TestBase
    {
        
        public WhenStatusRequestIsMadeNoAlba(WebApplicationFactory<Startup> factory) : base(factory)
        {
            
        }

        [Fact (Skip = "Not implemented")]
        public async void Will_return_ok_result()
        {
            var client = Factory.CreateClient();
            
            

            var response = await client.PostAsync("membershipcard/register",
                new StringContent(
                    "{\"EmployeeId\": \"whathehel\"," +
                    "\"FirstName\": \"TestName\"," +
                    "\"SecondName\": \"TestName\"," +
                    "\"MobileNumber\": \"0123456789\"}",
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

        }
    }
}