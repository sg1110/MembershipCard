using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Top_Up
{
    public class WhenPinIsNotCached : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        
        public WhenPinIsNotCached(WebApplicationFactory<Startup> factory) : base(factory)
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

        [Fact]
        public async void Will_return_no_sucess_response_code()
        { 
            var client = Factory.CreateClient();
            
         //   await LogIn(client);
          
            var response = await client.PutAsync("membershipcard/topup/1234dd7890123456",
                new StringContent(
                    "{\"TopUpAmount\": 100,}",
                    Encoding.UTF8, "application/json"));;
            
            response.EnsureSuccessStatusCode();

        }

        private static async Task LogIn(HttpClient client)
        {
            await client.PostAsync("membershipcard/login",
                new StringContent(
                    "{\"CardId\": \"1234dd7890123456\"," +
                    "\"CardPin\": \"1234\",}",
                    Encoding.UTF8, "application/json"));
        }
        
    }
}

