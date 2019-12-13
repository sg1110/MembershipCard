using Swashbuckle.AspNetCore.Examples;

namespace MembershipCardSystem.Registration.Model
{
    public class RegistrationRequestModelExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return new RegistrationRequest("b6Tzc0GFdYLNXOgfgeH2", "TestName", "TestSurname", "0123456789", "PzTvXogYxf6C38kf");
        }
    }
}