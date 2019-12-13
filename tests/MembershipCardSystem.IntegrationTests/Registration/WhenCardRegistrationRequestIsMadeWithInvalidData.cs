using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Http;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenCardRegistrationRequestIsMadeWithInvalidData : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardRegistrationRequestIsMadeWithInvalidData(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
        }

        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('IDIntegrationTest3')");
        }
        
        [Fact]
        public async void When_phone_number_is_missing_will_return_bad_request()
        {
            var client = Factory.CreateClient();
            
            
            var response = await client.PostAsync("card/register",
                new StringContent("{\"EmployeeId\": \"IDIntegrationTest\"," +
                                  "\"FirstName\": \"TestName\"," +
                                  "\"SecondName\": \"TestName\"," +
                                  "\"MobileNumber\": \"0123456789901234567891234567890\"," +
                                  "\"CardId\": \"ID34567890123456\"}"
                    ,
                    Encoding.UTF8, "application/json"));
            
            Assert.Equal((HttpStatusCode) StatusCodes.Status400BadRequest, response.StatusCode);
        }
        
        /// <summary>
        /// Currently tests contain a dynamic attribute.
        /// To improve this in the future, I would implement exception middleware with expected error response models
        /// </summary>
        [Fact]
        public async void When_phone_number_is_longer_than_expected_will_return_validation_error()
        {
            var client = Factory.CreateClient();
            
            
            var response = await client.PostAsync("card/register",
                new StringContent("{\"EmployeeId\": \"IDIntegrationTest\"," +
                                  "\"FirstName\": \"TestName\"," +
                                  "\"SecondName\": \"TestName\"," +
                                  "\"MobileNumber\": \"0123456789901234567891234567890\"}",
                    Encoding.UTF8, "application/json"));
            
            
            var controllerResponse = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()); 

            Assert.Equal("The field MobileNumber must be a string with a maximum length of 22.", (string) controllerResponse.errors.MobileNumber[0]);
        }
    }
}