using MembershipCardSystem.LogIn.Model;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.LogOut.Model
{
    public class LogOutResponseModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new LogOutResponse("Goodbye");

        }
    }
}