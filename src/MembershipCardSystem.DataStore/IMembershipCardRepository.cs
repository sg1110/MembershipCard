using System.Collections.Generic;
using System.Threading.Tasks;

namespace MembershipCardSystem.DataStore
{
    public interface IMembershipCardRepository
    {
        Task<IEnumerable<string>> GetAll();

        Task<IEnumerable<dynamic>> SaveRegistrationDetails(string employeeId,
            string firstName,
            string secondName,
            string mobileNumber);

        Task VerifyCardDetails(string presentCardCardId, string presentCardPin);
    }
}