using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Xunit;
using FluentAssertions;

namespace MembershipCardSystem.DataStore.IntegrationTest
{
    public class DataStoreTests
    {
        private readonly IDbConnection _connection;
        private readonly MembershipCardRepository _dataStore;
        private const string ConnectionString = "Server=localhost,1433;Database=Membership_Card;User Id=sa;Password=reallyStrongPwd123";


        public DataStoreTests()
        {
            _connection = new SqlConnection(ConnectionString);
            _dataStore = new MembershipCardRepository(_connection); 
            ClearTestData();
        }

        private void ClearTestData()
        {
            _connection.Execute("DELETE FROM [dbo].[Card] WHERE employee_id in ('Test2')");
        }

        [Fact]
        public async Task Save_registration_details()
        { 
            await _dataStore.SaveRegistrationDetails("Test2", "2Name", "2Surname", "01234567890");
            
            var selectSql = "Select first_name from dbo.Card WHERE employee_id in ('Test2')";
            var result = await _connection.QueryAsync(selectSql);
            var justName = result.Select(x => x.first_name);
            
            justName.Should().Equal("2Name");
        }
    }
}

