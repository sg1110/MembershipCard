namespace MembershipCardSystem.Status
{
    public class StatusSettings
    {
        public string Version { get; }
        public string Environment { get; }

        public StatusSettings(string version, string environment)
        {
            Version = version;
            Environment = environment;
        }
    }
}