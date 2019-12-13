using MembershipCardSystem.LogIn.Model;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.TopUp.Model
{
    public class TopUpRequestModelProvider : IExamplesProvider
    {
        public object GetExamples()
        {
            return new TopUpRequest( "100.10");
        }
    }
}