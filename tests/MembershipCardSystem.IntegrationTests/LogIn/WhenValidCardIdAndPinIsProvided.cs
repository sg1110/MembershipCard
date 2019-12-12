using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.LogIn
{
    public class WhenValidCardIdAndPinIsProvided : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        
        public WhenValidCardIdAndPinIsProvided(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithPin();
        }
        
        private void SetupTestUserWithPin()
        {
            var sql = "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id, pin) VALUES ('TestID9', 'TEST','mctest', '1234567890', '1234bb7890123456', '1234' )";

            _connection.Execute(sql);
        }
        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID9')");
        }

        [Fact]
        public async void Will_return_no_content()
        { 
            var client = Factory.CreateClient();
          
            var response = await client.PostAsync("membershipcard/login",
                new StringContent(
                    "{\"CardId\": \"1234bb7890123456\"," +
                    "\"CardPin\": \"1234\",}",
                    Encoding.UTF8, "application/json"));;
            
            response.EnsureSuccessStatusCode();
        }


    }
}


