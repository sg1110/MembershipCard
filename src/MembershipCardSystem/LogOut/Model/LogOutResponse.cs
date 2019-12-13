namespace MembershipCardSystem.LogOut.Model
{
    public class LogOutResponse
    {
        public LogOutResponse(string message)
        {
             Message = message;
        }

        public string Message { get; }
    }
}