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
        
        
        public async Task SaveRegistrationDetails(string employeeId,
            string firstName,
            string secondName,
            string mobileNumber,
            string cardId)
        {
            const string sprocName = "[dbo].[SaveCardDetailInformation]";
            
            await _connection.QueryAsync(sprocName, new
            {
                employee_id = employeeId,
                first_name = firstName,
                second_name = secondName,
                mobile_number = mobileNumber,
                card_id = cardId

            }, commandType: CommandType.StoredProcedure);
        }

        public async Task<Card> VerifyCardRegistration(string cardId)
        {
            const string sprocName = "[dbo].[GetCardIdandPin]";
            
            var cardDetails = await _connection.QueryAsync(sprocName, new
            {
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);

            if (cardDetails.Count() == 0) return new Card("", false);
            
            var dapperRow = cardDetails.FirstOrDefault();
            var allCardDetails= ((IDictionary<string, object>)dapperRow)?.Keys.ToArray();
            var details = ((IDictionary<string, object>)dapperRow);
            
            var storedCardId = (details?[allCardDetails[1]])?.ToString();
            var pin = (details?[allCardDetails[0]])?.ToString();

            var pinPresent = IsPinPresent(pin);
                            
            return new Card(storedCardId, pinPresent);
        }

        public async Task<string> GetPin(string cardId)
        {
            const string sprocName = "[dbo].[GetCardIdandPin]";

            var cardDetails = await _connection.QueryAsync(sprocName, new
            {
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);

            var dapperRow = cardDetails.FirstOrDefault();
            var allCardDetails= ((IDictionary<string, object>)dapperRow)?.Keys.ToArray();
            var details = ((IDictionary<string, object>)dapperRow);
            
            var pin = (details?[allCardDetails[0]])?.ToString();

            return pin;

        }

        public async Task<CardBalance> UpdateBalance(string cardId, string topUpAmount)
        {
            var currentBalance = await GetBalance(cardId);
            var increaseAmount = Convert.ToDecimal(topUpAmount);

            
            decimal newBalance = currentBalance + increaseAmount;

            const string sprocName = "[dbo].[SaveNewBalance]";
            
            await _connection.QueryAsync(sprocName, new
            {
                balance = newBalance,
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);
            
            return new CardBalance( newBalance.ToString());
        }

        public async Task<User> GetName(string cardId)
        {
            const string sprocName = "[dbo].[GetName]";
            
            var userName = await _connection.QueryAsync(sprocName, new
            {
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);
            
            var dapperRow = userName.FirstOrDefault();
            var cardDetails= ((IDictionary<string, object>)dapperRow)?.Keys.ToArray();
            var details = ((IDictionary<string, object>)dapperRow);
            var name = (details?[cardDetails[0]])?.ToString();
            
            return new User(name);

        }


        private async Task<decimal> GetBalance(string cardId)
        {
            const string sprocName = "[dbo].[GetBalance]";

            var cardBalance = await _connection.QueryAsync(sprocName, new
            {
                card_id = cardId
            }, commandType: CommandType.StoredProcedure);
            
            var dapperRow = cardBalance.FirstOrDefault();
            var cardDetails= ((IDictionary<string, object>)dapperRow)?.Keys.ToArray();
            var details = ((IDictionary<string, object>)dapperRow);
            var balance = (details?[cardDetails[0]])?.ToString();
            
            var intBalance = Convert.ToDecimal(balance);
            
            return intBalance;
        }
        

        //helper functions, move to controller?
        private static bool IsPinPresent(string pin)
        {
            return !string.IsNullOrEmpty(pin);
        }
    }
}