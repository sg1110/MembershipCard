using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.LogIn.Model
{
    public class LogInRequestModelExample: IExamplesProvider
    {
        public object GetExamples()
        {
            return new LogInRequest( "ID34567890123456", "1234");
        }
    }
}