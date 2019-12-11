using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Verify
{
    public class WhenCardRegisteredWithoutPinPresented : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString =
            "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        public WhenCardRegisteredWithoutPinPresented(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithoutAPin();

        }

        private void SetupTestUserWithoutAPin()
        {
            var sql =
                "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id) VALUES ('TestID2', 'TEST','mctest', '1234567890', '1234567890123456' )";

            _connection.Execute(sql);
        }

        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID2')");
        }

        [Fact]
        public  void Will_return_sucess_status_code()
        {
//            var client = Factory.CreateClient();
//
//            var response = await client.GetAsync("membershipcard/verify/0123456789012345");
//            response.EnsureSuccessStatusCode();
        }


        [Fact]
        public  void It_will_return_card_id_and_inform_that_pin_is_missing()
        {

        }


    }
}