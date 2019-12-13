using System.Collections.Generic;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore.Model;

namespace MembershipCardSystem.DataStore
{
    public interface IMembershipCardRepository
    {
        Task<IEnumerable<string>> GetAll();
        
        Task SaveRegistrationDetails(string employeeId,
            string firstName,
            string secondName,
            string mobileNumber,
            string cardId);
        
        Task<Card> VerifyCardRegistration(string cardId);
        
         Task<string> GetPin(string cardId);
 
         Task<CardBalance> UpdateBalance(string cardId, string topUpAmount);
         
         Task<User> GetName(string cardId);
    }
}