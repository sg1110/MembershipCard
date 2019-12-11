using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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

        public async Task<IEnumerable<dynamic>> SaveRegistrationDetails(string employeeId,
            string firstName,
            string secondName,
            string mobileNumber)
        {
            const string sprocName = "[dbo].[SaveCardDetailInformation]";


            var cardDetailsAdded = await _connection.QueryAsync(sprocName, new
            {
                employee_id = employeeId,
                first_name = firstName,
                second_name = secondName,
                mobile_number = mobileNumber,
                card_id = RandomString(16)

            }, commandType: CommandType.StoredProcedure);
            
//            addsqlexception

            return cardDetailsAdded;

        }

        public async Task<Card> VerifyCardRegistration(string cardId)
        {
            const string sprocName = "[dbo].[CheckCardDetails]";
            
            var cardDetails = await _connection.QueryAsync(sprocName, new
            {
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);
            
            
            var dapperRow = cardDetails.FirstOrDefault();
            var allCardDetails= ((IDictionary<string, object>)dapperRow).Keys.ToArray();
            var details = ((IDictionary<string, object>)dapperRow);
            var storedCardId = (details[allCardDetails[5]]).ToString();
            var pin = (details[allCardDetails[6]]).ToString();

            var pinPresent = IsPinPresent(pin);
                            
            return new Card(storedCardId, pinPresent);
        }

        public static bool IsPinPresent(string pin)
        {
            return !string.IsNullOrEmpty(pin);
        }
        
//To delete later because card if will be presented (ask for it in a body or route?)
        public static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}