namespace MembershipCardSystem.Status.Model
{
    public class ApplicationHealth
    {
        public string Version { get; }
        public string Environment { get; }
        public string Status { get; }

        public ApplicationHealth(string version, string environment, string status)
        {
            Version = version;
            Environment = environment;
            Status = status;
        }
    }
}