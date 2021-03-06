using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MembershipCardSystem.TopUp.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Top_Up
{
    public class WhenPinIsCached : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        
        public WhenPinIsCached(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithPin();
        }
        
        private void SetupTestUserWithPin()
        {
            var sql = "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id, pin) VALUES ('TestID11', 'TEST','mctest', '1234567890', '1234dd7890123456', '1234' )";

            _connection.Execute(sql);
        }
        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID11')");
        }

        private static async Task LogIn(HttpClient client)
        {
            await client.PostAsync("card/login",
                new StringContent(
                    "{\"CardId\": \"1234dd7890123456\"," +
                    "\"CardPin\": \"1234\",}",
                    Encoding.UTF8, "application/json"));
        }
        
        [Fact]
        public async void Will_return_sucess_response_code()
        {
            var client = Factory.CreateClient();
            await LogIn(client);
          
            var response = await client.PutAsync("card/topup/1234dd7890123456",
                new StringContent(
                    "{\"TopUpAmount\": \"100\",}",
                    Encoding.UTF8, "application/json"));;
            
            response.EnsureSuccessStatusCode();

        }
        
        [Fact]
        public async void Will_return_new_balance_information()
        { 
            var client = Factory.CreateClient();
            
            await LogIn(client);
          
            var response = await client.PutAsync("card/topup/1234dd7890123456",
                new StringContent(
                    "{\"TopUpAmount\": \"100.10\",}",
                    Encoding.UTF8, "application/json"));
            
            var controllerResponse = JsonConvert.DeserializeObject<TopUpResponse>(await response.Content.ReadAsStringAsync()); 
            
            Assert.Equal("100.10", controllerResponse.Balance);

        }

    }
}

