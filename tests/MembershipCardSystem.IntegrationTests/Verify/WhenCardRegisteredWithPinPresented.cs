using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using MembershipCardSystem.Verify.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Verify
{
    public class WhenCardIsRegisteredWithPin : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardIsRegisteredWithPin(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithPin();
          
        }

        private void SetupTestUserWithPin()
        {
            var sql = "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id, pin) VALUES ('TestID1', 'TEST','mctest', '1234567890', '1234aa7890123456', '1234' )";

            _connection.Execute(sql);
        }
        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID1')");
        }
        
        [Fact]
        public async void Will_return_sucess_status_code()
        {
            var client = Factory.CreateClient();

            var response = await client.GetAsync("card/verify/1234aa7890123456");
                response.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task It_will_return_card_id()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("card/verify/1234aa7890123456");
            
            var controllerResponse = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync()); 
            
            Assert.Equal("1234aa7890123456", (string) controllerResponse.cardId);
        
        //    ((CardRegistrationStatusResult) response.As<OkObjectResult>().Value).CardId.Should().Be("1234aa7890123456");
        
        }

        [Fact]
        public async Task It_will_return_true_for_registered_pin()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("card/verify/1234aa7890123456");
            
            var controllerResponse = JsonConvert.DeserializeObject<CardRegistrationVerificationResult>(await response.Content.ReadAsStringAsync()); 
            
            Assert.True((bool) controllerResponse.PinPresent);
        }
       
    }
}