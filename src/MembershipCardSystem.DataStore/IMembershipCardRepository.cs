using System.Collections.Generic;
using System.Threading.Tasks;

namespace MembershipCardSystem.DataStore
{
    public interface IMembershipCardRepository
    {
        Task<IEnumerable<string>> GetAll();
    }
}