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
    public class WhenCardIsRegisteredWithoutPin : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString =
            "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardIsRegisteredWithoutPin(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithoutAPin();

        }

        private void SetupTestUserWithoutAPin()
        {
            var sql =
                "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id) VALUES ('TestID2', 'TEST','mctest', '1234567890', 'ID34567890123456' )";

            _connection.Execute(sql);
        }

        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID2')");
        }

        [Fact]
        public async Task Will_return_sucess_status_code()
        {
            var client = Factory.CreateClient();

            var response = await client.GetAsync("membershipcard/verify/ID34567890123456");
            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public async Task It_will_return_card_id()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("membershipcard/verify/ID34567890123456");
            
            var controllerResponse = JsonConvert.DeserializeObject<CardRegistrationVerificationResult>(await response.Content.ReadAsStringAsync()); 
            
            Assert.Equal("ID34567890123456", (string) controllerResponse.CardId);

        }

        [Fact]
        public async Task It_will_return_false_for_registered_pin()
        {
            var client = Factory.CreateClient();
            var response = await client.GetAsync("membershipcard/verify/ID34567890123456");
            
            var controllerResponse = JsonConvert.DeserializeObject<CardRegistrationVerificationResult>(await response.Content.ReadAsStringAsync()); 
            
            Assert.False((bool) controllerResponse.PinPresent);
        }
    }
}