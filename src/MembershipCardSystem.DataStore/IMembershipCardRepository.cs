using System.Collections.Generic;
using System.Threading.Tasks;
using MembershipCardSystem.DataStore.Model;

namespace MembershipCardSystem.DataStore
{
    public interface IMembershipCardRepository
    {
        Task<IEnumerable<string>> GetAll();
        
        Task<IEnumerable<dynamic>> SaveRegistrationDetails(string employeeId,
            string firstName,
            string secondName,
            string mobileNumber);
        
        Task<Card> VerifyCardRegistration(string cardId);
        
         Task<string> GetPin(string cardId);
    }
}