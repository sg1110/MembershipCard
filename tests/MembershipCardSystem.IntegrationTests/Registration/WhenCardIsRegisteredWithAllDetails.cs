using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Registration
{
    public class WhenCardIsRegisteredWithAllDetails : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardIsRegisteredWithAllDetails(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
        }

        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('IdIdId1')");
        }
        
        [Fact]
        public async void Will_return_sucess_status_code()
        {
            var client = Factory.CreateClient();

            var response = await client.PostAsync("membershipcard/register",
                new StringContent(
                    "{\"EmployeeId\": \"IdIdId1\"," +
                    "\"FirstName\": \"TestName\"," +
                    "\"SecondName\": \"TestSurname\"," +
                    "\"MobileNumber\": \"0123456789\"," +
                    "\"CardId\": \"hdhdh2hdhdhdhdhd\"}",
                    Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
        }
    }
}