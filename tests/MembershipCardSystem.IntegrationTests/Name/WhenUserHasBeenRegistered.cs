using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MembershipCardSystem.LogIn.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace MembershipCardSystem.IntegrationTests.Name
{
    public class WhenUserHasBeenRegistered : TestBase
    {
        private readonly IDbConnection _connection;

        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";

        
        public WhenUserHasBeenRegistered(WebApplicationFactory<Startup> factory) : base(factory)
        {
            _connection = new SqlConnection(ConnectionString);
            ClearTestData();
            SetupTestUserWithPin();
        }
        
        private void SetupTestUserWithPin()
        {
            var sql = "INSERT INTO [dbo].[Card] (employee_id, first_name, second_name, mobile_number, card_id, pin) VALUES ('TestID1dd', 'Tester','Mctest', '1234567890', '123sdd7890123456', '1234' )";

            _connection.Execute(sql);
        }
        
        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('TestID1dd')");
        }
        

        [Fact]
        public async void Will_return_sucess_response_code()
        {
            var client = Factory.CreateClient();

            var response = await client.GetAsync("card/name/123sdd7890123456)");;
            
            response.EnsureSuccessStatusCode();

        }
        
        [Fact]
        public async void Will_return_users_name()
        { 
            var client = Factory.CreateClient();

            var response = await client.GetAsync("card/name/123sdd7890123456");
            var controllerResponse = JsonConvert.DeserializeObject<NameResponse>(await response.Content.ReadAsStringAsync()); 
            
            Assert.Equal("Tester", controllerResponse.UserName);

        }

    }
}

