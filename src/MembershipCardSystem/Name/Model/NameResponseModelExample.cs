using MembershipCardSystem.LogIn.Model;
using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.Name.Model
{
    public class NameResponseModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new NameResponse( "TestName");
        }
    }
}