using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenStatusRequestIsMadeNoAlba : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenStatusRequestIsMadeNoAlba(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
        }

        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('IDIntegrationTest')");
        }
        
        [Fact]
        public async void Will_return_sucess_status_code()
        {
            var client = Factory.CreateClient();
            
            
            var response = await client.PostAsync("membershipcard/register",
                new StringContent(
                    "{\"EmployeeId\": \"IDIntegrationTest\"," +
                    "\"FirstName\": \"TestName\"," +
                    "\"SecondName\": \"TestName\"," +
                    "\"MobileNumber\": \"0123456789\"}",
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}