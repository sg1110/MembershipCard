using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Dapper;
using MembershipCardSystem.DataStore.Model;

namespace MembershipCardSystem.DataStore
{
    public class MembershipCardRepository : IMembershipCardRepository
    {
        private readonly IDbConnection _connection;

        public MembershipCardRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<string>> GetAll()
        {
            var sql = "SELECT * FROM dbo.Card";
            var result = await _connection.QueryAsync<CardInfo>(sql);
            var justName = result.Select(x => x.first_name);
            return justName;

        }

        public async Task SaveRegistrationDetails(string employeeId, string firstName, string secondName,
            string mobileNumber,
            string cardId)
        {
            const string sprocName = "[dbo].[SaveCardDetailInformation]";


            var result = await _connection.QueryAsync(sprocName, new
            {
                employee_id = employeeId,
                first_name = firstName,
                second_name = secondName,
                mobile_number = mobileNumber,
                card_id = "2234567890123459"

            }, commandType: CommandType.StoredProcedure);
        }
    }
}