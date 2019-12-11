using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Verify
{
    public class WhenValidCardDetailPresented : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenValidCardDetailPresented(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
          
        }

        [Fact]
        public async void Will_return_sucess_status_code()
        {
            var client = Factory.CreateClient();

            var response = await client.GetAsync("membershipcard/verify/0123456789012345");
                response.EnsureSuccessStatusCode();
        }
    }
}