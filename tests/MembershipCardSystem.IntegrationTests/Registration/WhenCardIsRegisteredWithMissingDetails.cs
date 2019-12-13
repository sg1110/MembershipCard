using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MembershipCardSystem.Verify.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenCardIsRegisteredWithMissingDetails : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardIsRegisteredWithMissingDetails(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
        }

        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('IDIntegrationTest2')");
        }
        
        private async Task<HttpResponseMessage> RegisterTestUser()
        {
            var client = Factory.CreateClient();


            var response = await client.PostAsync("card/register",
                new StringContent(
                    "{\"EmployeeId\": \"IDIntegrationTest2\"," +
                    "\"FirstName\": \"TestName\"," +
                    "\"SecondName\": \"TestName\"}",
                    Encoding.UTF8, "application/json"));
            return response;
        }
        
        [Fact]
        public async void When_phone_number_is_missing_will_return_bad_request()
        {
            var response = await RegisterTestUser();

            Assert.Equal((HttpStatusCode) StatusCodes.Status400BadRequest, response.StatusCode);
        }

  

        /// <summary>
        /// Currently tests contain a dynamic attribute.
        /// To improve this in the future, I would implement exception middleware with expected error response models
        /// </summary>
        [Fact]
        public async void When_phone_number_is_missing_will_return_validation_error()
        {
            var response = await RegisterTestUser();

            var controllerResponse = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()); 

            Assert.Equal("The MobileNumber field is required.", (string) controllerResponse.errors.MobileNumber[0]);
        }
    }
}